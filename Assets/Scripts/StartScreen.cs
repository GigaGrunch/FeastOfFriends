using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

    public GameObject panel;
    bool showingHelp = false;

    // Use this for initialization
    void Start () {
        GetComponent<AudioScript>().playIntro();
	}
	
	// Update is called once per frame
	void Update () {
	    if (showingHelp && Input.GetMouseButton(0))
        {
            panel.SetActive(false);
            showingHelp = false;
        }
	}

    public void StartNewScreen()
    {
        Application.LoadLevel("ggj2016");
    }


    public void OpenHelp()
    {
        panel.SetActive(true);
        showingHelp = true;

    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
