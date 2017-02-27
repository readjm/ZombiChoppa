using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Gun : MonoBehaviour
{
    public AudioClip[] sounds;
    public Transform barrelTip;
    public GameObject muzzleFlash;
    public GameObject zombieHit;
    public GameObject groundHit;
    public float weaponDamage;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Debug.DrawRay(barrelTip.position, -barrelTip.forward * 100f);
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        if (muzzleFlash.GetComponentInChildren<Renderer>().material.color.a > 0)
        {
            foreach (Renderer renderer in muzzleFlash.GetComponentsInChildren<Renderer>())
            {
                renderer.material.color += new Color(0, 0, 0, -0.5f);
            }           
        }
    }

    public void Fire()
    {
        audioSource.clip = sounds[0];
        audioSource.Play();

        foreach (Renderer renderer in muzzleFlash.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color += new Color(0, 0, 0, 1);
        }
            
        RaycastHit hit;
        Ray gunRay = new Ray(barrelTip.position, -barrelTip.forward);


        if (Physics.Raycast(gunRay, out hit, 100f))
        {
            if (hit.collider.tag == "Zombie")
            {
                Instantiate(zombieHit, hit.point, Quaternion.identity);
                hit.collider.gameObject.GetComponent<AICharacterControl>().TakeDamage(weaponDamage);
            }
            else
            {
                GameObject impact = Instantiate(groundHit, hit.point, Quaternion.identity) as GameObject;
                Vector3 targetDir = transform.position - impact.transform.position;
                Vector3 hitRotation = Vector3.RotateTowards(impact.transform.forward, targetDir, 100.0f, 100.0f);
                impact.transform.rotation = Quaternion.LookRotation(hitRotation);
            }
        }
    }
}

