using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

    public Helicopter helicopter;
    public GameObject[] damagePanels;
    public GameObject deathScreen;
    public GameObject rescueScreen;
    public GameObject flare;
    public bool respawn = true;

    private AudioSource innerVoice;
    private Transform[] spawnPoints;
    private float health = 100.0f;
    private bool heliNear = false;
    private int damageIndex = 1;

    // Use this for initialization
    void Start ()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.priority == 1)
            {
                innerVoice = audioSource;
            }
        }

        spawnPoints = GameObject.Find("Player Spawn Points").GetComponentsInChildren<Transform>();
        Debug.Log("spawnPoints.Length: " + spawnPoints.Length);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (respawn)
        {
            Respawn();
            respawn = false;
        }

        if (heliNear && Input.GetKeyDown(KeyCode.E))
        {
            rescueScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<FirstPersonController>().enabled = false;
            GetComponentInChildren<Gun>().enabled = false;
        }
	}

    public void ThrowFlare()
    {
        GameObject thrownFlare = Instantiate(flare, transform.position + transform.right*.09f, transform.rotation) as GameObject;
        thrownFlare.GetComponent<Rigidbody>().velocity = Vector3.up*6f + transform.forward*11f;
        Debug.Log("Flare thrown");
    }

    void OnFindGoodLZ()
    {
        //DeployFlare
        //Spawn Zombies
    }

    void Respawn()
    {
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].position;
        Debug.Log("spawnIndex: " + spawnIndex);
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);

        damagePanels[damageIndex].GetComponent<DamagePanel>().Hit();
        damagePanels[0].GetComponent<Image>().color = new Color (255, 0, 0, (1f - (health * .01f)) * .5f);

        if (health <= 0)
        {
            deathScreen.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<FirstPersonController>().enabled = false;
            GetComponentInChildren<Gun>().enabled = false;
        }

        damageIndex++;
        if (damageIndex >= damagePanels.Length)
        {
            damageIndex = 1;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Helicopter")
        {
            Debug.Log("Reached helicopter");
            GameObject.Find("MessageBox").GetComponent<Text>().text = "Press E to enter helicopter";
            heliNear = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Helicopter")
        {
            Debug.Log("Left helicopter");
            GameObject.Find("MessageBox").GetComponent<Text>().text = "";
            heliNear = false;
        }
    }
}
