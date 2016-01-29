using UnityEngine;
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
    }
}
