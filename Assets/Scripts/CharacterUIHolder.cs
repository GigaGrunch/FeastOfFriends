using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CharacterUIHolder : MonoBehaviour
{
    bool selected;
    bool clickable;

    public void OnSelected()
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 1);
        selected = true;
    }

    public void OnDeselected()
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 0.35f);
        selected = false;
    }

    public void setSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void setClickable(bool clickable)
    {
        if (clickable)
        {
            this.clickable = true;
            OnDeselected();
        }
        else
        {
            this.clickable = false;
            GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        }
    }

    internal bool Toggle()
    {
        if (selected)
        {
            OnDeselected();
            return false;
        }
        else
        {
            OnSelected();
            return true;
        }
    }
}
