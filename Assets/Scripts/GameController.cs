using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    ArrayList<Room> activeRooms;
    Room selectedRoom;
    ArrayList<Character> selectedCharacters;

	void removeEmptyRooms()
    {
        foreach (Room i in activeRooms)
        {
            if (i.characters <= 0)
            {
                
            }
        }
    }
}
