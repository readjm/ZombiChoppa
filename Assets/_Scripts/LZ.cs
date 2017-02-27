using UnityEngine;
using System.Collections;

public class LZ : MonoBehaviour {

    public float timeSinceLastTrigger = 0;
    
    // Update is called once per frame
	void Update ()
    {
        timeSinceLastTrigger += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name != "Player")
        {
            timeSinceLastTrigger = 0;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name != "Player")
        {
            timeSinceLastTrigger = 0;
        }
    }

    public bool CheckGoodLZ()
    {
        Debug.Log("Checking LZ");
        if (timeSinceLastTrigger >= 1f)
        {
            Debug.Log("Good LZ");
            return true;
        }
        else
        {
            Debug.Log("Bad LZ");
            return false;
        }
    }
}
