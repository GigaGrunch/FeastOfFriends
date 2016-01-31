using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CharacterUIHolder : MonoBehaviour
{
    bool selected;
    bool clickable;
    GameObject arrow;

    void Start()
    {
        arrow = transform.GetChild(0).gameObject;
        arrow.SetActive(false);
    }

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

    public void SetArrow(int direction)
    {
        arrow.SetActive(true);
        switch (direction)
        {
            case 0:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                arrow.transform.Rotate(0, 0, 90);
                break;
            case 2:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                arrow.transform.Rotate(0, 0, 180);
                break;
            case 3:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                arrow.transform.Rotate(0, 0, 270);
                break;
            default:
                arrow.SetActive(false);
                break;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void SetClickable(bool clickable)
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
