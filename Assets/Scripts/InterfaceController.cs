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

    [SerializeField]
    GameObject statsBars;

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
            c.GetComponent<CharacterUIHolder>().SetClickable(false);
            c.GetComponent<CharacterUIHolder>().SetSprite(null);
            c.GetComponent<CharacterUIHolder>().SetArrow(-1);
        }

        FindObjectOfType<GameController>().onCharacterClicked(null);

        cPreview.enabled = false;
        cName.text = "";

        for (int i = 0; i < 6; i++)
        {
            if (roomMembers.Count <= i)
            {
                return;
            }
            characterButtons[i].GetComponent<CharacterUIHolder>().SetClickable(true);
            characterButtons[i].GetComponent<CharacterUIHolder>().SetSprite(roomMembers[i].Portrait);

            if (roomMembers[i].arrow != null && roomMembers[i].arrow.activeSelf)
            {
                switch ((int)roomMembers[i].arrow.transform.eulerAngles.z)
                {
                    case 0:
                        characterButtons[i].GetComponent<CharacterUIHolder>().SetArrow(0);
                        break;
                    case 90:
                        characterButtons[i].GetComponent<CharacterUIHolder>().SetArrow(1);
                        break;
                    case 180:
                        characterButtons[i].GetComponent<CharacterUIHolder>().SetArrow(2);
                        break;
                    case 270:
                        characterButtons[i].GetComponent<CharacterUIHolder>().SetArrow(3);
                        break;
                }
            }

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
            FindObjectOfType<GameController>().onCharacterClicked(characters[position]);
            FindObjectOfType<GameController>().SelectedRoom.SelectedCharacters.Add(characters[position]);
            cPreview.enabled = true;
            cPreview.sprite = characters[position].Face;
            cName.text = characters[position].CharName;
        }
        else
        {
            int i = 0;
            List<Character> selectedCharacters = FindObjectOfType<GameController>().SelectedRoom.SelectedCharacters;
            foreach (Character c in selectedCharacters)
            {
                if (characters[position].CharName == c.CharName)
                {
                    selectedCharacters.RemoveAt(i);
                    break;
                }
                i++;
            }
            if (selectedCharacters.Count > 0)
            {
                cPreview.sprite = selectedCharacters[selectedCharacters.Count - 1].Face;
                cName.text = selectedCharacters[selectedCharacters.Count - 1].CharName;
                FindObjectOfType<GameController>().onCharacterClicked(selectedCharacters[selectedCharacters.Count - 1]);
            }
            else
            {
                cPreview.enabled = false;
                cName.text = "";
                FindObjectOfType<GameController>().onCharacterClicked(null);
            }
        }
    }

    internal void ShowStatsBar(bool shouldShow)
    {
        statsBars.SetActive(shouldShow);
    }
}
