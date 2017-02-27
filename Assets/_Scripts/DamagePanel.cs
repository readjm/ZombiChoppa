using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamagePanel : MonoBehaviour
{

    private RawImage image;

    // Use this for initialization
	void Start ()
    {

         image = GetComponent<RawImage>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if  (image.color.a > 0)
        {
            image.color -= new Color(0, 0, 0, .01f);
        }
	}

    public void Hit()
    {
        image.color = new Color(255,255,255, 1);
    }
}
