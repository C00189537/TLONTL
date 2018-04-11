using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    private AudioManager() { }

    public AudioSource audiosource;

    public AudioClip falling;
    public AudioClip Jumping;
    public AudioClip Collectable;
    public AudioClip HitObstacle;
    public AudioClip CorrectStep;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        //DontDestroyOnLoad( this.gameObject );
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    	
	// Update is called once per frame
	void Start () {
        audiosource = GetComponent<AudioSource>();
	}
}
