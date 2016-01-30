using UnityEngine;
using System.Collections;

public class Story {

    private int dayNum;
    private string storyText;

    public Story(int dayNum, string storyText)
    {
        this.dayNum = dayNum;
        this.storyText = storyText;
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
