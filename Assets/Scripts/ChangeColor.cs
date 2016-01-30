using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

    [SerializeField]
    Sprite[] sprites = new Sprite[3];

    [SerializeField]
    float interval = 0.2f;

    int currentIndex;
    SpriteRenderer spriteRenderer;
    float lastChange = 0;
    int numSprites;

    void Start()
    {
        interval = Random.Range(0.05f, 0.5f);
        numSprites = sprites.Length;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > lastChange + interval)
        {
            lastChange = Time.time;
            spriteRenderer.sprite = sprites[(++currentIndex) % numSprites];
        }
	}
}
