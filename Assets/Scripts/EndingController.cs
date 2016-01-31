using UnityEngine;
using System.Collections;

public class EndingController : MonoBehaviour
{
    public static bool playerWon = false;

    [SerializeField]
    Sprite winSprite;
    [SerializeField]
    Sprite loseSprite;

    // Use this for initialization
    void Start () {
        GameObject endScreen = GameObject.Find("ende");
        if (playerWon)
        {
            endScreen.GetComponent<SpriteRenderer>().sprite = winSprite;
        }
        else
        {
            endScreen.GetComponent<SpriteRenderer>().sprite = loseSprite;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
