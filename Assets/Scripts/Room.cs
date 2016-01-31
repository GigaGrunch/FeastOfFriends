using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    [SerializeField]
    Vector2[] spawnField = new Vector2[6];

    [SerializeField]
    GameObject charSprite, arrow;

    [SerializeField]
    float spawnRangeFactor = 0.1f;

    public Reward[] reward;
    private Requirement[] requirement;
    private bool revealed;

    //List<GameObject> arrows = new List<GameObject>();
    //GameObject[] arrows = new GameObject[6];
    GameObject arrow0, arrow1, arrow2, arrow3, arrow4, arrow5;

    List<GameObject> sprites = new List<GameObject>();
    List<Character> characters = new List<Character>();
    List<Requirement> requirements = new List<Requirement>();
    private GameController gameController;
    List<Character> selectedCharacters = new List<Character>();
    List<Movement> pendingMovementsNorth = new List<Movement>();
    List<Movement> pendingMovementsEast = new List<Movement>();
    List<Movement> pendingMovementsSouth = new List<Movement>();
    List<Movement> pendingMovementsWest = new List<Movement>();

    List<GameObject> fades = new List<GameObject>();

    [SerializeField]
    GameObject WallTop, WallBottom, WallLeft, WallRight;
    [SerializeField]
    GameObject CornerTopLeft, CornerTopRight, CornerBottomLeft, CornerBottomRight;
    [SerializeField]
    GameObject DoorLeft, DoorRight, DoorTop, DoorBottom;
    [SerializeField]
    GameObject Tile;
    [SerializeField]
    GameObject TunnelHor, TunnelVer;
    [SerializeField]
    GameObject Fade;
    [SerializeField]
    GameObject TunnelRand1, TunnelRand2;

    [SerializeField]
    GameObject Furniture, Furniture_used;

    [SerializeField]
    bool isKitchen;

    [SerializeField]
    Room northRoom;
    [SerializeField]
    Room eastRoom;
    [SerializeField]
    Room westRoom;
    [SerializeField]
    Room southRoom;
    [SerializeField]
    int BonusFactor = 4;

    [SerializeField]
    GameObject blackSmog;

    [SerializeField]
    GameObject selectBubble;

    public void Initialize()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        transform.Translate(transform.position.y / 4 * -.5f, transform.position.x / 4 * .5f, 0);

        GameObject temp;

        gameController = FindObjectOfType<GameController>();

        temp = Instantiate(CornerBottomLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, -.75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerBottomRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, -.75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerTopLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerTopRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerTopLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, -.25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, -.25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, .25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, .25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallBottom, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, -.75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallTop, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, -.25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        int turned = 0;

        if (northRoom != null)
        {
            temp = Instantiate(DoorTop, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, .75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (isKitchen)
            {
                Furniture.transform.Rotate(0, 0, 270);
                ++turned;
            }

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-.25f, 1.25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-.25f, 1.75f, 0);
            //tunnels.Add(temp);
        }
        else
        {
            temp = Instantiate(WallTop, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, .75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }

        if (eastRoom != null)
        {
            temp = Instantiate(DoorRight, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(1.25f, .25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(1.75f, .25f, 0);
            //tunnels.Add(temp);
            if (isKitchen && turned == 1)
            {
                Furniture.transform.Rotate(0, 0, 270);
                ++turned;
            }
        }
        else
        {
            temp = Instantiate(WallRight, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }

        if (southRoom != null)
        {
            temp = Instantiate(DoorBottom, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(.25f, -1.25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(.25f, -1.75f, 0);
            //tunnels.Add(temp);
            if (isKitchen && turned == 2)
            {
                Furniture.transform.Rotate(0, 0, 270);
                ++turned;
            }
        }
        else
        {
            temp = Instantiate(WallBottom, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;


        }

        if (westRoom != null)
        {
            temp = Instantiate(DoorLeft, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-1.25f, -.25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-1.75f, -.25f, 0);
            //tunnels.Add(temp);
        }
        else
        {
            temp = Instantiate(WallLeft, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        }
    }

    public List<Character> Characters
    {
        get
        {
            return characters;
        }

        set
        {
            characters = value;
        }
    }

    public void drawPeople()
    {
        foreach (GameObject i in sprites)
        {
            Destroy(i);
        }

        foreach (Character i in characters)
        {
            if (i.arrow != null)
                i.arrow.SetActive(false);
        }

        //arrows.Clear();
        sprites.Clear();

        GameObject temp;

        for (int i = 0; i < characters.Count; ++i)
        {
            temp = Instantiate(charSprite);
            temp.transform.position = transform.position;
            temp.transform.Translate(spawnField[i]);
            temp.transform.Translate(Random.insideUnitCircle * spawnRangeFactor);
            temp.GetComponent<SpriteRenderer>().sprite = characters[i].Portrait;

            sprites.Add(temp);
        }
    }

    public List<Requirement> Requirements
    {
        get
        {
            return requirements;
        }

        set
        {
            requirements = value;
        }
    }

    public Reward[] Reward
    {
        get
        {
            return reward;
        }

        set
        {
            reward = value;
        }
    }

    public Room NorthRoom
    {
        get
        {
            return northRoom;
        }

        set
        {
            northRoom = value;
        }
    }

    public Room EastRoom
    {
        get
        {
            return eastRoom;
        }

        set
        {
            eastRoom = value;
        }
    }

    public Room WestRoom
    {
        get
        {
            return westRoom;
        }

        set
        {
            westRoom = value;
        }
    }

    public Room SouthRoom
    {
        get
        {
            return southRoom;
        }

        set
        {
            southRoom = value;
        }
    }

    public List<Character> SelectedCharacters
    {
        get
        {
            return selectedCharacters;
        }

        set
        {
            selectedCharacters = value;
        }
    }

    public List<GameObject> Fades
    {
        get
        {
            return fades;
        }

        set
        {
            fades = value;
        }
    }

    public GameObject BlackSmog
    {
        get
        {
            return blackSmog;
        }

        set
        {
            blackSmog = value;
        }
    }

    public GameObject SelectBubble
    {
        get
        {
            return selectBubble;
        }

        set
        {
            selectBubble = value;
        }
    }

    public Requirement[] Requirement
    {
        get
        {
            return requirement;
        }

        set
        {
            requirement = value;
        }
    }

    public bool Revealed
    {
        get
        {
            return revealed;
        }

        set
        {
            revealed = value;
        }
    }

    // left mouse button
    void OnMouseDown()
    {
        if (FindObjectOfType<CameraController>().ClickBlockedByUI())
        {
            return;
        }
        //discoverNeighbors();

        if (gameController.SelectedRoom != this)
        {
            gameController.onRoomSelected(this);
        }
    }

    void OnMouseOver()
    {
        if (GameObject.FindObjectOfType<CameraController>().ClickBlockedByUI())
        {
            return;
        }

        // right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Room selectedRoom = gameController.SelectedRoom;
            List<Character> deselectedCharacters = new List<Character>();

            // can only move to neighbouring rooms
            if (selectedRoom.NorthRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    clearMovement(movingChar, selectedRoom);

                    selectedRoom.pendingMovementsNorth.Add(new Movement(selectedRoom, this, movingChar, 0));

                    movingChar.arrow.transform.position = selectedRoom.sprites[selectedRoom.characters.IndexOf(movingChar)].transform.position;
                    movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                    movingChar.arrow.transform.Translate(0, 0.25f, 0);
                    movingChar.arrow.SetActive(true);
                    deselectedCharacters.Add(movingChar);


                    //arrows.Add(temp);
                }
            }
            else if (selectedRoom.EastRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    clearMovement(movingChar, selectedRoom);

                    selectedRoom.pendingMovementsEast.Add(new Movement(selectedRoom, this, movingChar, 1));

                    movingChar.arrow.transform.position = selectedRoom.sprites[selectedRoom.characters.IndexOf(movingChar)].transform.position;
                    movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                    movingChar.arrow.transform.Translate(0.25f, 0, 0);
                    movingChar.arrow.transform.Rotate(0, 0, 270);
                    movingChar.arrow.SetActive(true);
                    deselectedCharacters.Add(movingChar);
                    //arrows.Add(temp);
                }
            }
            else if (selectedRoom.WestRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    clearMovement(movingChar, selectedRoom);

                    selectedRoom.pendingMovementsWest.Add(new Movement(selectedRoom, this, movingChar, 3));

                    //Destroy(arrows[index]);
                    //arrows[index] = Instantiate(arrow);

                    movingChar.arrow.transform.position = selectedRoom.sprites[selectedRoom.characters.IndexOf(movingChar)].transform.position;
                    movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                    movingChar.arrow.transform.Translate(-.25f, 0, 0);
                    movingChar.arrow.transform.Rotate(0, 0, 90);
                    //arrows.Add(temp);
                    movingChar.arrow.SetActive(true);
                    deselectedCharacters.Add(movingChar);
                }
            }
            else if (selectedRoom.SouthRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    clearMovement(movingChar, selectedRoom);

                    selectedRoom.pendingMovementsSouth.Add(new Movement(selectedRoom, this, movingChar, 2));

                    //Destroy(arrows[index]);
                    //arrows[index] = Instantiate(arrow);

                    movingChar.arrow.transform.position = selectedRoom.sprites[selectedRoom.characters.IndexOf(movingChar)].transform.position;
                    movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                    movingChar.arrow.transform.Translate(0, -0.25f, 0);
                    movingChar.arrow.transform.Rotate(0, 0, 180);
                    movingChar.arrow.SetActive(true);
                    deselectedCharacters.Add(movingChar);
                    //arrows.Add(temp);
                }
            }

            foreach (Character c in deselectedCharacters)
            {
                selectedRoom.selectedCharacters.Remove(c);
            }
            FindObjectOfType<InterfaceController>().SetRoomMembers(selectedRoom.characters);
        }
    }

    private void clearMovement(Character movingChar, Room currentRoom)
    {
        for (int i = 0; i < currentRoom.pendingMovementsEast.Count; i++)
        {
            if (currentRoom.pendingMovementsEast[i].Character == movingChar)
            {
                currentRoom.pendingMovementsEast.Remove(currentRoom.pendingMovementsEast[i]);
            }
        }

        for (int i = 0; i < currentRoom.pendingMovementsWest.Count; i++)
        {
            if (currentRoom.pendingMovementsWest[i].Character == movingChar)
            {
                currentRoom.pendingMovementsWest.Remove(currentRoom.pendingMovementsWest[i]);
            }
        }

        for (int i = 0; i < currentRoom.pendingMovementsNorth.Count; i++)
        {
            if (currentRoom.pendingMovementsNorth[i].Character == movingChar)
            {
                currentRoom.pendingMovementsNorth.Remove(currentRoom.pendingMovementsNorth[i]);
            }
        }

        for (int i = 0; i < currentRoom.pendingMovementsSouth.Count; i++)
        {
            if (currentRoom.pendingMovementsSouth[i].Character == movingChar)
            {
                currentRoom.pendingMovementsSouth.Remove(currentRoom.pendingMovementsSouth[i]);
            }
        }
    }

    public void resolvePendingMovements()
    {
        if (pendingMovementsEast.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsEast);
        }
        if (pendingMovementsSouth.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsSouth);
        }
        if (pendingMovementsWest.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsWest);
        }
        if (pendingMovementsNorth.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsNorth);
        }

    }

    private void tryToExecuteMovement(List<Movement> pendingMovements)
    {
        // test requirements here
        Requirement[] destinationRequirements = pendingMovements[0].Destination.Requirement;

        if (doMovementsFulfillRequirement(pendingMovements, destinationRequirements))
        {
            foreach (Movement pendingMovement in pendingMovements)
            {
                gameController.OnPlayerMovementSuccess(pendingMovement.Source, pendingMovement.Destination, pendingMovement.Character);
            }
            pendingMovements[0].Destination.Requirement = null;
            gameController.OnMovementToRoomSuccess(pendingMovements[0].Destination);
        }
        else
        {
            pendingMovements[0].Destination.Revealed = true;
        }
        pendingMovements.Clear();
    }

    public bool doMovementsFulfillRequirement(List<Movement> movements, Requirement[] requirements)
    {
        int type = 0;
        int required = 0;
        int bonus = 0;
        bool success = false;
        List<Character> movingChars = new List<Character>();

        foreach (Movement movement in movements)
        {
            if (requirements == null || requirements.Length == 0)
            {
                return true;
            }
            Character character = movement.Character;
            foreach (Requirement r in requirements)
            {
                if (r.IsActive)
                {
                    required = r.getRequiredValue();

                    if (r.getType() == global::Requirement.Type.agility)
                    {
                        if (character.Agility >= r.getRequiredValue())
                        {
                            bonus = r.requiredValue / (BonusFactor * 2);
                            character.Agility += bonus;
                            if (bonus <= 0) bonus = 1;
                            type = 1;
                            success = true;
                            movingChars.Add(character);
                        }
                        else
                        {
                            type = 1;
                            success = false;
                            movingChars.Add(character);
                        }
                    }
                    else if (r.getType() == global::Requirement.Type.strength)
                    {
                        if (character.Strength >= r.getRequiredValue())
                        {
                            bonus = r.requiredValue / (BonusFactor * 2);
                            character.Strength += bonus;
                            if (bonus <= 0) bonus = 1;
                            type = 2;
                            success = true;
                            movingChars.Add(character);
                        }
                        else
                        {
                            type = 2;
                            success = false;
                            movingChars.Add(character);
                        }
                    }
                    r.IsActive = true;
                }
            }
        }

        string names = "";

        if (movingChars.Count > 0)
        {
            foreach (Character c in movingChars)
            {
                names += c.CharName + ", ";
            }

            names = names.Remove(names.Length - 2);
        }

        if (type == 1)
        {
            if (success)
            {
                gameController.writeRequirementStory(names + " managed to overcome an exciting obstacle because of their dominating Agility! As a result their Agility has even increased by " + bonus + ".");
                gameController.audio.playObstAgi();
            }
            else
            {
                gameController.writeRequirementStory(names + " failed to overcome an exciting obstacle because of their meagre Agility! One of them would have needed " + required + " Agility to master it.");
                gameController.audio.playObstFail();
            }
        }
        if (type == 2)
        {
            if (success)
            {
                gameController.writeRequirementStory(names + " managed to overcome an exciting obstacle because of their dominating Strength. As a result their Strength has even increased by " + bonus + ".");
                gameController.audio.playObstStr();
            }
            else
            {
                gameController.writeRequirementStory(names + " failed to overcome an exciting obstacle because of their meagre Strength! One of them would have needed " + (required) + " more Agility to master it.");
                gameController.audio.playObstFail();
            }
        }

        return success;
    }

    public bool sacrifice(int currentDayNum, Journal journal)
    {
        if (Reward[0].IsActive && selectedCharacters.Count > 0 && characters.Count >= 2)
        {
            Furniture_used.transform.rotation = Furniture.transform.rotation;

            Furniture_used.SetActive(true);
            Furniture.SetActive(false);

            Character victim = selectedCharacters[selectedCharacters.Count - 1];
            string storyText = victim.CharName + " gave his live for the greater Good!";
            selectedCharacters.Remove(victim);
            characters.Remove(victim);
            gameController.killCharacter(victim);
            drawPeople();
            int strengthBonus = victim.Strength / BonusFactor;
            int agilityBonus = victim.Agility / BonusFactor;
            int visionBonus = victim.Vision / BonusFactor;
            storyText += "\n";

            foreach (Character c in characters)
            {
                c.feast(strengthBonus, agilityBonus, visionBonus);
                storyText += c.CharName;
                if (characters.IndexOf(c) != characters.Count - 1)
                {
                    storyText += ", ";
                }
            }
            if(strengthBonus == 0)
            {
                strengthBonus = 1;
            }
            if (agilityBonus == 0)
            {
                agilityBonus = 1;
            }
            if (visionBonus == 0)
            {
                visionBonus = 1;
            }
            storyText += " gained " + visionBonus + " Vision, " + strengthBonus + " Strength and " + agilityBonus + " Agility from feasting on his flesh";
            journal.addStory(new Story(currentDayNum, storyText));

            FindObjectOfType<InterfaceController>().SetRoomMembers(characters);
            Reward[0].IsActive = false;
            return true;
        }
        return false;

    }

    public void discoverNeighbors()
    {
        gameObject.SetActive(true);

        GameObject temp;
        BlackSmog.SetActive(false);
        foreach (GameObject i in fades)
        {
            Destroy(i);
        }
        fades.Clear();

        if (NorthRoom != null)
        {
            temp = Instantiate(TunnelRand2, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 2, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
            temp.transform.Rotate(0, 0, 90);

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 1.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 1.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 2.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 2.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (northRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(-.25f, 3f, 0);
                temp.transform.Rotate(0, 0, 180);
                NorthRoom.Fades.Add(temp);
            }

            NorthRoom.gameObject.SetActive(true);
        }
        if (SouthRoom != null)
        {
            temp = Instantiate(TunnelRand2, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -2, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
            temp.transform.Rotate(0, 0, 90);

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -1.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -1.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -2.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -2.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (southRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(.25f, -3f, 0);
                temp.transform.Rotate(0, 0, 0);
                SouthRoom.Fades.Add(temp);
            }

            SouthRoom.gameObject.SetActive(true);
        }
        if (WestRoom != null)
        {
            temp = Instantiate(TunnelRand1, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-2, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 5;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-1.25f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-1.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-2.25f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-2.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (WestRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(-3f, -.25f, 0);
                temp.transform.Rotate(0, 0, 270);
                WestRoom.Fades.Add(temp);
            }

            WestRoom.gameObject.SetActive(true);
        }
        if (EastRoom != null)
        {
            temp = Instantiate(TunnelRand1, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(2, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 5;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(1.25f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(1.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(2.25f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(2.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (EastRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(3f, .25f, 0);
                temp.transform.Rotate(0, 0, 90);
                EastRoom.Fades.Add(temp);
            }

            EastRoom.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (EastRoom == null || SelectedCharacters.Count == 0)
            {
                return;
            }

            foreach (Character movingChar in SelectedCharacters)
            {
                clearMovement(movingChar, this);
                pendingMovementsEast.Add(new Movement(this, EastRoom, movingChar, 1));
                movingChar.arrow.transform.position = sprites[characters.IndexOf(movingChar)].transform.position;
                movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                movingChar.arrow.transform.Translate(0.25f, 0, 0);
                movingChar.arrow.transform.Rotate(0, 0, 270);
                movingChar.arrow.SetActive(true);
            }
            selectedCharacters.Clear();
            FindObjectOfType<InterfaceController>().SetRoomMembers(characters);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (WestRoom == null || SelectedCharacters.Count == 0)
            {
                return;
            }

            foreach (Character movingChar in SelectedCharacters)
            {
                clearMovement(movingChar, this);
                pendingMovementsWest.Add(new Movement(this, WestRoom, movingChar, 3));
                movingChar.arrow.transform.position = sprites[characters.IndexOf(movingChar)].transform.position;
                movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                movingChar.arrow.transform.Translate(-.25f, 0, 0);
                movingChar.arrow.transform.Rotate(0, 0, 90);
                movingChar.arrow.SetActive(true);
            }
            selectedCharacters.Clear();
            FindObjectOfType<InterfaceController>().SetRoomMembers(characters);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (NorthRoom == null || SelectedCharacters.Count == 0)
            {
                return;
            }

            foreach (Character movingChar in SelectedCharacters)
            {
                clearMovement(movingChar, this);
                pendingMovementsNorth.Add(new Movement(this, NorthRoom, movingChar, 0));
                movingChar.arrow.transform.position = sprites[characters.IndexOf(movingChar)].transform.position;
                movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                movingChar.arrow.transform.Translate(0, 0.25f, 0);
                movingChar.arrow.SetActive(true);
            }
            selectedCharacters.Clear();
            FindObjectOfType<InterfaceController>().SetRoomMembers(characters);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (SouthRoom == null || SelectedCharacters.Count == 0)
            {
                return;
            }

            foreach (Character movingChar in SelectedCharacters)
            {
                clearMovement(movingChar, this);
                pendingMovementsSouth.Add(new Movement(this, SouthRoom, movingChar, 2));
                movingChar.arrow.transform.position = sprites[characters.IndexOf(movingChar)].transform.position;
                movingChar.arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                movingChar.arrow.transform.Translate(0, -0.25f, 0);
                movingChar.arrow.transform.Rotate(0, 0, 180);
                movingChar.arrow.SetActive(true);
            }
            selectedCharacters.Clear();
            FindObjectOfType<InterfaceController>().SetRoomMembers(characters);
        }
    }
}