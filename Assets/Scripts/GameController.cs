﻿using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int currentDayNum;
    int nextDeathIn;

    [SerializeField]
    Room selectedRoom;
    [SerializeField]
    Sprite[] playerHeads;
    [SerializeField]
    Sprite[] playerFaces;

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

    [SerializeField]
    Text AgilityValue, StrengthValue, VisionValue;

    [SerializeField]
    GameObject turnbar;
    [SerializeField]
    Sprite[] turnbarSprites;

    public new AudioScript audio;

    void Start()
    {
        audio = GetComponent<AudioScript>();

        // initialize start characters here
        currentDayNum = 1;
        nextDeathIn = 5;
        // TODO: replace with Constructor if Journal is no gameobject
        journal = FindObjectOfType<Journal>();
        journal.gameObject.SetActive(false);
        FindObjectOfType<InterfaceController>().HideRoomStats();

        List<string> playerNames = loadPlayerNames();

        selectedRoom = GameObject.Find("Start (2,3)").GetComponent<Room>();
        List<int> usedIndizes = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            int randomInt = UnityEngine.Random.Range(0, playerNames.Count);
            Character testCharacter = Instantiate(character_prefab);
            testCharacter.CharName = playerNames[randomInt];
            playerNames.RemoveAt(randomInt);
            selectedRoom.Characters.Add(testCharacter);
            do
            {
                randomInt = UnityEngine.Random.Range(0, playerHeads.Length);
            } while (usedIndizes.Contains(randomInt));

            usedIndizes.Add(randomInt);
            testCharacter.Portrait = playerHeads[randomInt];
            testCharacter.Face = playerFaces[randomInt];
            characters.Add(testCharacter);
        }

        selectedRoom.drawPeople();

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
            StreamReader reader = new StreamReader("names.txt", Encoding.Default);
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

    public GameObject SacrificeButton
    {
        get
        {
            return sacrificeButton;
        }

        set
        {
            sacrificeButton = value;
        }
    }

    public void onCharacterClicked(Character clickedCharacter)
    {
        if (clickedCharacter != null)
        {
            FindObjectOfType<InterfaceController>().ShowStatsBar(true);
        }
        else
        {
            FindObjectOfType<InterfaceController>().ShowStatsBar(false);
        }

        AgilityBar.sizeDelta = new Vector2(clickedCharacter != null ? clickedCharacter.Agility * 3.667f : 0, 10);
        StrenghtBar.sizeDelta = new Vector2(clickedCharacter != null ? clickedCharacter.Strength * 3.667f : 0, 10);
        VisionBar.sizeDelta = new Vector2(clickedCharacter != null ? clickedCharacter.Vision * 3.667f : 0, 10);

        AgilityValue.text = "" + (clickedCharacter != null ? clickedCharacter.Agility : 0);
        StrengthValue.text = "" + (clickedCharacter != null ? clickedCharacter.Strength : 0);
        VisionValue.text = "" + (clickedCharacter != null ? clickedCharacter.Vision : 0);


        if (selectedRoom == this && selectedRoom.Reward.Length > 0 && selectedRoom.Reward[0].getType().Equals(Reward.Type.altar))
        {
            if (selectedRoom.Characters.Count < 2)
            {
                sacrificeButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                sacrificeButton.GetComponent<Button>().interactable = true;
            }
        }
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

        Reward[] rewards = SelectedRoom.Reward;
        bool altarExists = false;
        bool disableSacrifice = false;
        foreach (Reward r in rewards)
        {
            if (r.getType() == Reward.Type.altar && activeRooms.Contains(selectedRoom))
            {
                altarExists = true;
                if (!r.IsActive || selectedRoom.Characters.Count <= 1)
                {
                    disableSacrifice = true;
                }
                break;
            }
        }

        if (SelectedRoom.BlackSmog.activeSelf)
        {
            if (SelectedRoom.Revealed)
            {
                FindObjectOfType<InterfaceController>().ShowAllRoomStats(SelectedRoom.Reward, SelectedRoom.Requirement);
            }
            else
            {
                List<Character> neighbourChars = new List<Character>();
                if (clickedRoom.NorthRoom != null)
                {
                    neighbourChars.AddRange(clickedRoom.NorthRoom.Characters);
                }
                if (clickedRoom.EastRoom != null)
                {
                    neighbourChars.AddRange(clickedRoom.EastRoom.Characters);
                }
                if (clickedRoom.SouthRoom != null)
                {
                    neighbourChars.AddRange(clickedRoom.SouthRoom.Characters);
                }
                if (clickedRoom.WestRoom != null)
                {
                    neighbourChars.AddRange(clickedRoom.WestRoom.Characters);
                }

                FindObjectOfType<InterfaceController>().ShowSomeRoomStats(SelectedRoom.Reward, SelectedRoom.Requirement, neighbourChars);
            }
        }
        else
        {
            FindObjectOfType<InterfaceController>().HideRoomStats();
        }

        SacrificeButton.SetActive(altarExists);
        SacrificeButton.GetComponent<Button>().interactable = !disableSacrifice;

        FindObjectOfType<InterfaceController>().SetRoomMembers(clickedRoom.Characters);
    }

    public void endTurn()
    {
        // do some stuff on turn end
        foreach (Room r in activeRooms)
        {
            r.resolvePendingMovements();
        }

        foreach (Room i in activeRooms)
        {
            i.drawPeople();

            i.SelectedCharacters.Clear();
        }

        removeEmptyRooms();
        foreach (Room r in roomsToActivate)
        {
            if (!activeRooms.Contains(r))
            {
                activeRooms.Add(r);
            }
        }

        foreach (Room i in activeRooms)
        {
            i.drawPeople();

            i.SelectedCharacters.Clear();
        }

        currentDayNum++;
        nextDeathIn--;

        if (nextDeathIn <= 0)
        {
            killRandomCharacter();
            audio.playRandomSac();
            nextDeathIn = 5;
            turnbar.GetComponent<Image>().sprite = turnbarSprites[4];
        }
        else
        {
            turnbar.GetComponent<Image>().sprite = turnbarSprites[nextDeathIn-1];
        }
        foreach (Character c in characters)
        {
            c.IsCurrentlyMoving = false;
        }

        FindObjectOfType<InterfaceController>().SetRoomMembers(SelectedRoom.Characters);
        onRoomSelected(SelectedRoom);
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
            i.drawPeople();
        }
        killCharacter(victim);
        journal.addStory(new Story(currentDayNum, "Due to your inability to do the necessary your beloved companion " + victim.CharName + " died a pointless death"));
    }

    public void killCharacter(Character victim)
    {
        characters.Remove(victim);
        Destroy(victim.gameObject);
        if (characters.Count == 0)
        {
            EndingController.playerWon = false;
            SceneManager.LoadScene("EndScene");
        }
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
        int capacity = 6;
        if (destination.Reward != null && destination.Reward.Length > 0 && destination.Reward[0].getType() == Reward.Type.human)
        {
            capacity = 5;
        }
        if (destination.Characters.Count < capacity)
        {
            source.Characters.Remove(character);
            destination.Characters.Add(character);
        }

    }

    public void sacrifice()
    {
        if(selectedRoom.sacrifice(currentDayNum, journal))
        {
            audio.playFeast(); ;
            nextDeathIn = 5;
            turnbar.GetComponent<Image>().sprite = turnbarSprites[4];
            onRoomSelected(selectedRoom);
        }
    }

    public void print(string name)
    {
        Debug.Log(name);
    }

    public void OnMovementToRoomSuccess(Room destination)
    {
        if (destination.name.Equals("Finish"))
        {
            EndingController.playerWon = true;
            SceneManager.LoadScene("EndScene");
        }

        roomsToActivate.Add(destination);

        destination.discoverNeighbors();

        if (destination.Reward != null && destination.Reward.Length > 0)
        {
            // TODO: remove [0] with proper selection mechanism if multiple rewards are possible
            Reward reward = destination.Reward[0];
            if (reward.IsActive && reward.getType() == Reward.Type.human)
            {
                List<string> playerNames = loadPlayerNames();
                string playerName;
                Sprite playerHead;
                Sprite playerFace;
                do
                {
                    playerName = playerNames[UnityEngine.Random.Range(0, playerNames.Count)];

                } while (!playerWithNameDoesNotExist(playerName));
                do
                {
                    int randomInt = UnityEngine.Random.Range(0, playerHeads.Length);
                    playerHead = playerHeads[randomInt];
                    playerFace = playerFaces[randomInt];

                } while (!playerWithHeadDoesNotExist(playerHead));
                Character newCharacter = Instantiate(character_prefab);
                audio.playNewGuy();
                newCharacter.CharName = playerName;
                newCharacter.Portrait = playerHead;
                newCharacter.Face = playerFace;
                characters.Add(newCharacter);
                destination.Characters.Add(newCharacter);
                destination.drawPeople();
                reward.IsActive = false;
                journal.addStory(new Story(currentDayNum, "In the corner of the room you find a lost soul. " + playerName + " joins your party of gourmets. He looks quite tasty."));
            }
        }
    }

    private bool playerWithHeadDoesNotExist(Sprite playerHead)
    {
        foreach (Character c in characters)
        {
            if (c.Portrait.Equals(playerHead))
            {
                return false;
            }
        }
        return true;
    }

    private bool playerWithNameDoesNotExist(string playerName)
    {
        foreach (Character c in characters)
        {
            if (c.CharName.Equals(playerName))
            {
                return false;
            }
        }
        return true;
    }

    public void writeRequirementStory(String storyText)
    {
        journal.addStory(new Story(currentDayNum, storyText));
    }
}
