using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    Room selectedRoom;
    List<Room> activeRooms = new List<Room>();
    List<Character> selectedCharacters = new List<Character>();

    void Start()
    {
        // inititalize start characters here
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

    public void onCharacterClicked(Character clickedCharacter)
    {
        if(selectedCharacters.Contains(clickedCharacter))
        {
            selectedCharacters.Remove(clickedCharacter);
        }
        else
        {
            selectedCharacters.Add(clickedCharacter);
        }
    }

    public void onRoomClicked(Room clickedRoom)
    {
        selectedRoom = clickedRoom;
    }

    void endTurn()
    {
        // do some stuff on turn end
        removeEmptyRooms();
    }
}
