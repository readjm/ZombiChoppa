using UnityEngine;
using System.Collections;

public class StartAmbiance : MonoBehaviour {

    private AudioSource audioSource;

	// Use this for initialization
	void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
