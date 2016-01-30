using UnityEngine;
using System.Collections.Generic;

public class MapGen : MonoBehaviour {

    [SerializeField]
    GameObject normalRoom, Kitchen;

    List<Room> rooms = new List<Room>();

	// Use this for initialization
	void Start () {
        Room start;

        rooms.Add(addRoom(normalRoom, 0, 0, false, true, false, false));
        rooms.Add(addRoom(normalRoom, 0, 1, false, true, false, false));

        Room kitchen = addRoom(Kitchen, 0, 2, true, false, false, false);
        kitchen.Reward = new Reward[1];
        kitchen.Reward[0] = new Reward
        {
            type = Reward.Type.altar,
            visionRequired = 0
        };
        rooms.Add(kitchen);

        rooms.Add(addRoom(normalRoom, 0, 3, true, true, true, false));
        rooms.Add(addRoom(normalRoom, 0, 4, true, false, true, false));
        rooms.Add(addRoom(normalRoom, 0, 5, false, true, true, false));

        rooms.Add(addRoom(normalRoom, 1, 0, false, true, false, true));
        rooms.Add(addRoom(normalRoom, 1, 1, true, false, false, true));
        rooms.Add(addRoom(normalRoom, 1, 2, true, true, true, false));
        rooms.Add(addRoom(normalRoom, 1, 3, false, true, true, true));
        rooms.Add(addRoom(normalRoom, 1, 4, true, true, false, false));
        rooms.Add(addRoom(normalRoom, 1, 5, false, false, true, true));

        rooms.Add(addRoom(normalRoom, 2, 0, true, true, false, true));
        rooms.Add(addRoom(normalRoom, 2, 1, true, true, true, false));
        rooms.Add(addRoom(normalRoom, 2, 2, true, false, true, true));
        rooms.Add(start = addRoom(normalRoom, 2, 3, true, true, true, true));
        rooms.Add(addRoom(normalRoom, 2, 4, true, true, true, true));

        kitchen = addRoom(Kitchen, 2, 5, false, true, true, false);
        kitchen.Reward = new Reward[1];
        kitchen.Reward[0] = new Reward
        {
            type = Reward.Type.altar,
            visionRequired = 0
        };
        rooms.Add(kitchen);

        rooms.Add(addRoom(normalRoom, 3, 0, false, false, false, true));
        rooms.Add(addRoom(normalRoom, 3, 1, false, true, false, true));
        rooms.Add(addRoom(normalRoom, 3, 3, false, true, false, true));
        rooms.Add(addRoom(normalRoom, 3, 4, false, true, false, true));
        rooms.Add(addRoom(normalRoom, 3, 5, false, true, false, true));

        rooms.Add(addRoom(normalRoom, 4, 0, false, true, false, false));
        rooms.Add(addRoom(normalRoom, 4, 1, true, false, false, true));
        rooms.Add(addRoom(normalRoom, 4, 2, true, false, true, true));
        rooms.Add(addRoom(normalRoom, 4, 3, false, true, true, true));
        rooms.Add(addRoom(normalRoom, 4, 4, false, true, false, true));
        rooms.Add(addRoom(normalRoom, 4, 5, false, true, false, true));

        rooms.Add(addRoom(normalRoom, 5, 0, true, false, false, true));
        rooms.Add(addRoom(normalRoom, 5, 1, true, false, true, false));
        rooms.Add(addRoom(normalRoom, 5, 2, true, false, true, false));
        rooms.Add(addRoom(normalRoom, 5, 3, true, false, true, true));

        kitchen = addRoom(Kitchen, 5, 4, true, false, true, true);
        kitchen.Reward = new Reward[1];
        kitchen.Reward[0] = new Reward
        {
            type = Reward.Type.altar,
            visionRequired = 0
        };
        rooms.Add(kitchen);

        rooms.Add(addRoom(normalRoom, 5, 5, false, false, true, true));

        foreach (Room i in rooms)
        {
            i.Initialize();
        }

        start.name = "Start (2,3)";
        start.gameObject.SetActive(true);
    }
	
	Room addRoom(GameObject type, int x, int y, bool north, bool east, bool south, bool west)
    {
        GameObject temp = Instantiate(type, new Vector3(x * 4, y * 4, 0), Quaternion.identity) as GameObject;
        Room result = temp.GetComponent<Room>();

        result.name = "Room (" + x + "," + y + ")";

        if (north)
        {
            temp = GameObject.Find("Room (" + x + "," + (y + 1) + ")");

            if (temp != null)
            {
                result.NorthRoom = temp.GetComponent<Room>();
                temp.GetComponent<Room>().SouthRoom = result;
            }
        }

        if (east)
        {
            temp = GameObject.Find("Room (" + (x+1) + "," + y + ")");

            if (temp != null)
            {
                result.EastRoom = temp.GetComponent<Room>();
                temp.GetComponent<Room>().WestRoom = result;
            }
        }

        if (south)
        {
            temp = GameObject.Find("Room (" + x + "," + (y - 1) + ")");

            if (temp != null)
            {
                result.SouthRoom = temp.GetComponent<Room>();
                temp.GetComponent<Room>().NorthRoom = result;
            }
        }

        if (west)
        {
            temp = GameObject.Find("Room (" + (x - 1) + "," + y + ")");

            if (temp != null)
            {
                result.WestRoom = temp.GetComponent<Room>();
                temp.GetComponent<Room>().EastRoom = result;
            }
        }

        return result;
    }
}
