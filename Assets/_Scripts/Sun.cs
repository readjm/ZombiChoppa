using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{

    [Tooltip ("Set seconds per second")]
    public float timeScale;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate sun according to timescale (seconds per second)
        transform.Rotate(new Vector3(Time.deltaTime*timeScale*.00416f, 0, 0));
	}
}
