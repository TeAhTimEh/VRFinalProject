using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songPlayer : MonoBehaviour {

    //zelda, suns, storms, eponas, time, saria 
    public AudioClip[] songs;
    AudioSource aus;

	// Use this for initialization
	void Start () {
        aus = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playSong(int song)
    {
            aus.clip = songs[song];
            aus.PlayDelayed(1.5f);
    }
}
