using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    GameObject one;
    GameObject two;
    GameObject three;

    public bool showingCredits = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (showingCredits)
        {
            if (Input.GetMouseButton(0))
            {
                if (one.activeSelf)
                {
                    one.SetActive(false);
                    two.SetActive(true);
                }
                if (two.activeSelf)
                {
                    two.SetActive(false);
                    three.SetActive(true);
                }
                if (three.activeSelf)
                {
                    three.SetActive(false);
                    showingCredits = false;
                }
            }
        }
	}

    void showCredits()
    {
        showingCredits = true;
        one.SetActive(true);
    }
}
