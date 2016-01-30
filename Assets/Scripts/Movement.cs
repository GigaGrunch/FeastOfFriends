using UnityEngine;
using System.Collections;

public class Movement {

    private Room source;
    private Room destination;
    private Character character;

    public Movement(Room source, Room destination, Character character)
    {
        this.Source = source;
        this.Destination = destination;
        this.Character = character;
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

    public void OnSuccess()
    {
        Source.Characters.Remove(Character);
        Destination.Characters.Add(Character);
    }
}
