using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
    


	// Use this for initialization
	void Start () {
        GetComponent<AudioScript>().playIntro();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartNewScreen()
    {
        Application.LoadLevel("ggj2016");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
