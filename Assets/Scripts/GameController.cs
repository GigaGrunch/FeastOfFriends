﻿using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Sprite face;
    [SerializeField]
    Sprite face2;

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

    void Start()
    {
        // initialize start characters here
        currentDayNum = 1;
        nextDeathIn = 5;
        // TODO: replace with Constructor if Journal is no gameobject
        journal = FindObjectOfType<Journal>();

        List<string> playerNames = loadPlayerNames();
        int randomInt = UnityEngine.Random.Range(0, playerNames.Count);
        Character testCharacter1 = new Character();
        testCharacter1.Portrait = playerHeads[0];
        testCharacter1.CharName = playerNames[randomInt];
        playerNames.RemoveAt(randomInt);
        randomInt = UnityEngine.Random.Range(0, playerNames.Count);
        Character testCharacter2 = new Character();
        testCharacter2.Portrait = playerHeads[1];
        testCharacter2.CharName = playerNames[randomInt];
        playerNames.RemoveAt(randomInt);
        randomInt = UnityEngine.Random.Range(0, playerNames.Count);
        Character testCharacter3 = new Character();
        testCharacter3.Portrait = playerHeads[2];
        testCharacter3.CharName = playerNames[randomInt];
        playerNames.RemoveAt(randomInt);
        randomInt = UnityEngine.Random.Range(0, playerNames.Count);
        Character testCharacter4 = new Character();
        testCharacter4.Portrait = playerHeads[3];
        testCharacter4.CharName = playerNames[randomInt];
        playerNames.RemoveAt(randomInt);
        randomInt = UnityEngine.Random.Range(0, playerNames.Count);
        Character testCharacter5 = new Character();
        testCharacter5.Portrait = playerHeads[4];
        testCharacter5.CharName = playerNames[randomInt];
        playerNames.RemoveAt(randomInt);
        randomInt = UnityEngine.Random.Range(0, playerNames.Count);
        Character testCharacter6 = new Character();
        testCharacter6.Portrait = playerHeads[5];
        testCharacter6.CharName = playerNames[randomInt];
        playerNames.RemoveAt(randomInt);

        foreach (Room i in FindObjectsOfType<Room>())
        {
            i.BlackSmog.SetActive(true);
            i.gameObject.SetActive(false);
        }

        selectedRoom.SelectBubble.SetActive(true);
        selectedRoom.discoverNeighbors();

        selectedRoom.Characters.Add(testCharacter1);
        selectedRoom.Characters.Add(testCharacter3);
        selectedRoom.Characters.Add(testCharacter4);
        selectedRoom.Characters.Add(testCharacter5);
        selectedRoom.Characters.Add(testCharacter6);
        characters.Add(testCharacter1);
        characters.Add(testCharacter2);
        characters.Add(testCharacter3);
        characters.Add(testCharacter4);

        GameObject.Find("Room (2)").GetComponent<Room>().Characters.Add(testCharacter2);

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
        if (SelectedRoom.SelectedCharacters.Contains(clickedCharacter))
        {
            SelectedRoom.SelectedCharacters.Remove(clickedCharacter);
        }
        else
        {
            SelectedRoom.SelectedCharacters.Add(clickedCharacter);
        }
    }

    public void onRoomSelected(Room clickedRoom)
    {
        if (SelectedRoom != null)
        {
            SelectedRoom.SelectBubble.SetActive(false);
        }
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

        currentDayNum++;
        nextDeathIn--;
        if (nextDeathIn <= 0)
        {
            killRandomCharacter();
            nextDeathIn = 5;
        }
        foreach(Character c in characters)
        {
            c.IsCurrentlyMoving = false;
        }
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
        foreach (Room i in activeRooms)
        {
            if (i.Characters.Count <= 0)
            {
                activeRooms.Remove(i);
            }
        }
    }

    public void OnPlayerMovementSuccess(Room source, Room destination, Character character)
    {
        Debug.Log(character.name + " successfully moved to " + destination.name);
        source.Characters.Remove(character);
        destination.Characters.Add(character);

        journal.addStory(new Story(currentDayNum, character + " managed to enter an exciting new Room"));
        activeRooms.Remove(source);
        if (!activeRooms.Contains(destination))
        {
            activeRooms.Add(destination);
            if (destination.NorthRoom != null)
            {
                destination.NorthRoom.enabled = true;
            }
            if (destination.SouthRoom != null)
            {
                destination.SouthRoom.enabled = true;
            }
            if (destination.WestRoom != null)
            {
                destination.WestRoom.enabled = true;
            }
            if (destination.EastRoom != null)
            {
                destination.EastRoom.enabled = true;
            }
        }
    }
}
