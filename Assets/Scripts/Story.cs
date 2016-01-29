using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {

    private int dayNum;
    private string storyText;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int DayNum
    {
        get
        {
            return dayNum;
        }

        set
        {
            dayNum = value;
        }
    }

    public string StoryText
    {
        get
        {
            return storyText;
        }

        set
        {
            storyText = value;
        }
    }
}
