using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int currentDayNum;
    int nextDeathIn;

    [SerializeField]
    Room selectedRoom;
    [SerializeField]
    Sprite[] playerHeads;

    List<Room> activeRooms = new List<Room>();
    Journal journal;
    List<Character> characters = new List<Character>();

    [SerializeField]
    GameObject sacrificeButton;

    List<Room> roomsToActivate = new List<Room>();

    [SerializeField]
    Character character_prefab;

    [SerializeField]
    RectTransform AgilityBar, StrenghtBar, VisionBar;

    void Start()
    {
        // initialize start characters here
        currentDayNum = 1;
        nextDeathIn = 5;
        // TODO: replace with Constructor if Journal is no gameobject
        //journal = FindObjectOfType<Journal>();
        //journal.gameObject.SetActive(false);

        List<string> playerNames = loadPlayerNames();

        selectedRoom = GameObject.Find("Start (2,3)").GetComponent<Room>();

        for (int i = 0; i < 6; i++)
        {
            int randomInt = UnityEngine.Random.Range(0, playerNames.Count);
            Character testCharacter = Instantiate(character_prefab);
            testCharacter.Portrait = playerHeads[0];
            testCharacter.CharName = playerNames[randomInt];
            playerNames.RemoveAt(randomInt);
            selectedRoom.Characters.Add(testCharacter);
        }

        foreach (Room i in FindObjectsOfType<Room>())
        {
            i.BlackSmog.SetActive(true);
            i.gameObject.SetActive(false);
        }

        selectedRoom.SelectBubble.SetActive(true);
        selectedRoom.discoverNeighbors();

        activeRooms.Add(selectedRoom);

        FindObjectOfType<InterfaceController>().SetRoomMembers(selectedRoom.Characters);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            endTurn();
        }
    }

    private List<string> loadPlayerNames()
    {
        List<string> result = new List<string>();
        try
        {
            StreamReader reader = new StreamReader("Assets/names.txt", Encoding.Default);
            string line;
            using (reader)
            {
                do
                {
                    line = reader.ReadLine();

                    if (line != null)
                    {
                        result.Add(line);
                    }
                }
                while (line != null);
                reader.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return result;

        }
        return result;
    }

    public Room SelectedRoom
    {
        get
        {
            return selectedRoom;
        }

        set
        {
            selectedRoom = value;
        }
    }

    public void onCharacterClicked(Character clickedCharacter)
    {
        Debug.Log("Setting bars...");

        AgilityBar.sizeDelta = new Vector2(clickedCharacter != null ? clickedCharacter.Agility * 3.667f : 0, 10);
        StrenghtBar.sizeDelta = new Vector2(clickedCharacter != null ? clickedCharacter.Strength * 3.667f : 0, 10);
        VisionBar.sizeDelta = new Vector2(clickedCharacter != null ? clickedCharacter.Vision * 3.667f : 0, 10);
    }

    public void onRoomSelected(Room clickedRoom)
    {
        if (SelectedRoom != null)
        {
            SelectedRoom.SelectBubble.SetActive(false);
        }
        SelectedRoom.SelectedCharacters.Clear();
        SelectedRoom = clickedRoom;
        SelectedRoom.SelectBubble.SetActive(true);
        sacrificeButton.SetActive(SelectedRoom.Rewards.Exists(ByType(Reward.Type.altar)));

        FindObjectOfType<InterfaceController>().SetRoomMembers(clickedRoom.Characters);
    }

    static Predicate<Reward> ByType(Reward.Type type)
    {
        return delegate (Reward reward)
        {
            return reward.getType() == type;
        };
    }

    public void endTurn()
    {
        Debug.Log("END TURN " + currentDayNum);
        // do some stuff on turn end
        foreach (Room r in activeRooms)
        {
            r.resolvePendingMovements();
        }
        removeEmptyRooms();
        foreach (Room r in roomsToActivate)
        {
            activeRooms.Add(r);
        }

        currentDayNum++;
        nextDeathIn--;
        if (nextDeathIn <= 0)
        {
            killRandomCharacter();
            nextDeathIn = 5;
        }
        foreach (Character c in characters)
        {
            c.IsCurrentlyMoving = false;
        }

        FindObjectOfType<InterfaceController>().SetRoomMembers(SelectedRoom.Characters);
    }

    private void killRandomCharacter()
    {
        // kill random character without boni for the others
        int index = UnityEngine.Random.Range(0, characters.Count);
        Character victim = characters[index];
        foreach (Room i in activeRooms)
        {
            i.SelectedCharacters.Remove(victim);
            i.Characters.Remove(victim);
        }
        killCharacter(victim);
    }

    public void killCharacter(Character victim)
    {
        characters.Remove(victim);
        Destroy(victim.gameObject);
    }

    void removeEmptyRooms()
    {
        List<Room> toRemove = new List<Room>();
        foreach (Room i in activeRooms)
        {
            if (i.Characters.Count <= 0)
            {
                //activeRooms.Remove(i);
                toRemove.Add(i);
            }
        }
        foreach (Room r in toRemove)
        {
            activeRooms.Remove(r);
        }
    }

    public void OnPlayerMovementSuccess(Room source, Room destination, Character character)
    {
        source.Characters.Remove(character);
        destination.Characters.Add(character);

        //journal.addStory(new Story(currentDayNum, character.CharName + " managed to enter an exciting new Room"));
        destination.discoverNeighbors();
    }
}
