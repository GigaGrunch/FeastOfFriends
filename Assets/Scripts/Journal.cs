using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Journal : MonoBehaviour {

    private List<Story> stories = new List<Story>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Story> Stories
    {
        get
        {
            return stories;
        }

        set
        {
            stories = value;
        }
    }

    public void addStory(Story newStory)
    {
        stories.Add(newStory);
        int storyNumber = stories.Count;
        Text[] children = gameObject.GetComponentsInChildren<Text>();
        foreach(Text child in children)
        {
            if(child.gameObject.name.Equals("Entry" + storyNumber + "Headline"))
            {
                child.text = "Day " + newStory.DayNum + " after the Awakening";
            }
            else if(child.gameObject.name.Equals("Entry" + storyNumber + "Text"))
            {
                child.text = newStory.StoryText;
            }
        }
    }
}
