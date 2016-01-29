using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    List<Room> activeRooms = new List<Room>();
    Room selectedRoom;
    List<Character> selectedCharacters = new List<Character>();

	void removeEmptyRooms()
    {
        foreach (Room i in activeRooms)
        {
            if (i.characters <= 0)
            {
                activeRooms.Remove(i);
            }
        }
    }
}
