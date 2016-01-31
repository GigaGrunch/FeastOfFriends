using UnityEngine;
using System.Collections;

public class Movement {

    private Room source;
    private Room destination;
    private Character character;
    private int direction; // 0 == north; 1 == east; 2 == south; 3 == west;

    public Movement(Room source, Room destination, Character character, int direction)
    {
        this.Source = source;
        this.Destination = destination;
        this.Character = character;
        this.Direction = direction;
    }

    public Room Source
    {
        get
        {
            return source;
        }

        set
        {
            source = value;
        }
    }

    public Room Destination
    {
        get
        {
            return destination;
        }

        set
        {
            destination = value;
        }
    }

    public Character Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }

    public int Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public void OnSuccess()
    {
        Source.Characters.Remove(Character);
        Destination.Characters.Add(Character);
    }
}
