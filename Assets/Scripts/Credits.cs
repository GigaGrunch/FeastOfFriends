using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    [SerializeField]
    GameObject one, two, three;

    public bool showingCredits = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (showingCredits)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (three.activeSelf)
                {
                    three.SetActive(false);
                    showingCredits = false;
                }
                if (two.activeSelf)
                {
                    two.SetActive(false);
                    three.SetActive(true);
                }
                if (one.activeSelf)
                {
                    one.SetActive(false);
                    two.SetActive(true);
                }
                
                
            }
        }
	}

    public void showCredits()
    {
        showingCredits = true;
        one.SetActive(true);
    }
}
