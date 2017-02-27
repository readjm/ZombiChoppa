using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {
    public AudioClip[] heliSounds;
    public LZ lz;
    public float speed;
    public float pitchSpeed;
    public float rollSpeed;
    public float yawSpeed;

    private AudioSource audioSource;
    //private Rigidbody rigidBody;
    private bool heliCalled = false;
    private Vector3 target;
    private Vector3 target_Level;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        //rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("CallHeli") && lz.CheckGoodLZ() && !heliCalled)
        {
            GameObject.Find("Player").GetComponent<Player>().ThrowFlare();
            audioSource.clip = heliSounds[0];
            audioSource.Play();
            CallHeli();
            Debug.Log("Good LZ");
        }
        else if(Input.GetButtonUp("CallHeli") && !heliCalled)
        {
            audioSource.clip = heliSounds[1];
            audioSource.Play();
            Debug.Log("Bad LZ");
        }

        if (heliCalled)
        {
            Vector3 currentTarget;
            if (Vector3.Distance(transform.position, target) <= 50f)
            {
                speed = 10f;
                currentTarget = target;
            }
            else if (Vector3.Distance(transform.position, target) <= 200f)
            {
                speed = 25f;
                currentTarget = target;
            }
            else
            {
                currentTarget = target_Level;
            }

            MoveHeli(currentTarget);
        }
    }

    public void CallHeli()
    {
        heliCalled = true;
        target = lz.transform.position;
        target.y -= 2.5f;
        target_Level = new Vector3(target.x, transform.position.y, target.z);
        lz.gameObject.SetActive(false);
        Debug.Log("Helicopter inbound");
    }

    void MoveHeli(Vector3 currentTarget)
    {
        Vector3 targetDir = target - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed*Time.deltaTime);
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetDir, yawSpeed*Time.deltaTime, 0.0F);
        newRotation.y = 0;
        transform.rotation = Quaternion.LookRotation(newRotation);
    }

}
