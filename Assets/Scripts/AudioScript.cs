using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

    [SerializeField]
    AudioClip intro, volunteer, newGuy, obstFail;

    [SerializeField]
    AudioClip[] randomSacrifice, gameOver, win, obstStr, obstAgi;

    new AudioSource audio;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	public void playRandomSac()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = getRandomClip(randomSacrifice);
        audio.Play();
    }

    public void playIntro()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = intro;
        audio.Play();
    }

    public void playGameOver()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = getRandomClip(gameOver);
        audio.Play();
    }

    public void playWin()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = getRandomClip(win);
        audio.Play();
    }

    public void playObstStr()
    {
        if (!audio.isPlaying)
        {
            audio.clip = getRandomClip(obstStr);
            audio.Play();
        }
    }

    public void playObstAgi()
    {
        if (!audio.isPlaying)
        {
            audio.clip = getRandomClip(obstAgi);
            audio.Play();
        }
    }

    public void playObstFail()
    {
        if (!audio.isPlaying)
        {
            audio.clip = obstFail;
            audio.Play();
        }
    }

    public void playNewGuy()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = newGuy;
        audio.Play();
    }

    public void playFeast()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = volunteer;
        audio.Play();
    }

    AudioClip getRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length - 1)];
    }
}
