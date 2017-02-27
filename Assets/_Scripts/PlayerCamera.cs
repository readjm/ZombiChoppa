using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    private Camera playerCamera;
    private float defaultFOV;
    // Use this for initialization
	void Start ()
    {
        playerCamera = GetComponent<Camera>();
        defaultFOV = playerCamera.fieldOfView;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetButton("Zoom"))
        {
            Zoom(1.5f);
        }
        else
        {
            Zoom(1.0f);
        }

	}

     void Zoom(float multiplier)
    {
        playerCamera.fieldOfView = defaultFOV / multiplier;
    }
}
