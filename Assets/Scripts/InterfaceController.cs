using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InterfaceController : MonoBehaviour
{
    [SerializeField]
    GameObject[] characterButtons = new GameObject[6];
    Image[] cButtons = new Image[6];

    [SerializeField]
    GameObject preview;

    [SerializeField]
    GameObject characterName;

    Image cPreview;
    Text cName;

    Character[] characterIcons;

    void Start()
    {
        cPreview = preview.GetComponent<Image>();
        cName = characterName.GetComponent<Text>();

        for (int i = 0; i < 6; i++)
        {
            cButtons[i] = characterButtons[i].GetComponent<Image>();
        }
    }

    public void SetRoomMembers(List<Character> roomMembers)
    {
        foreach (GameObject c in characterButtons)
        {
            c.GetComponent<CharacterUIHolder>().setClickable(false);
            c.GetComponent<CharacterUIHolder>().setSprite(null);
        }

        cPreview.enabled = false;
        cName.text = "";

        for (int i = 0; i < 6; i++)
        {
            if (roomMembers.Count <= i)
            {
                return;
            }
            characterButtons[i].GetComponent<CharacterUIHolder>().setClickable(true);
            characterButtons[i].GetComponent<CharacterUIHolder>().setSprite(roomMembers[i].Portrait);
        }
    }

    public void SelectCharacter(int position)
    {
        List<Character> characters = FindObjectOfType<GameController>().SelectedRoom.Characters;

        if (characters.Count <= position)
        {
            return;
        }

        bool selected = characterButtons[position].GetComponent<CharacterUIHolder>().Toggle();

        if (selected)
        {
            FindObjectOfType<GameController>().SelectedRoom.SelectedCharacters.Add(characters[position]);
            cPreview.enabled = true;
            cPreview.sprite = characters[position].Portrait;
            cName.text = characters[position].CharName;
        }
        else
        {
            List<Character> selectedCharacters = FindObjectOfType<GameController>().SelectedRoom.SelectedCharacters;
            Debug.Log(selectedCharacters.Count);
            selectedCharacters.Remove(characters[position]);
            Debug.Log(selectedCharacters.Count);
            if (selectedCharacters.Count > 0)
            {
                cPreview.sprite = selectedCharacters[(selectedCharacters.Count) - 1].Portrait;
                cName.text = selectedCharacters[(selectedCharacters.Count) - 1].CharName;
            }
            else
            {
                cPreview.enabled = false;
                cName.text = "";
            }
        }
    }
}
