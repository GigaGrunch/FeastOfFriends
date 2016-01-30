using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InterfaceController : MonoBehaviour
{
    [SerializeField]
    GameObject[] characterButtons = new GameObject[6];
    Button[] cButtons = new Button[6];

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
            cButtons[i] = characterButtons[i].GetComponent<Button>();
        }
    }

    void Update()
    {
    }

    public void SetRoomMembers(List<Character> roomMembers)
    {
        for (int i = 0; i < 6; i++)
        {
            if (roomMembers.Count <= i)
            {
                return;
            }
            characterButtons[i].GetComponent<Image>().sprite = roomMembers[i].Portrait;
        }
    }

    public void SelectCharacter(int position)
    {
        List<Character> characters = FindObjectOfType<GameController>().SelectedRoom.Characters;

        if (characters.Count <= position)
        {
            return;
        }

        cPreview.sprite = characters[position].Portrait;
        cName.text = characters[position].CharName;
    }
}
