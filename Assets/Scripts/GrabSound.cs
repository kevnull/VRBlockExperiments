using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabSound : MonoBehaviour {

    public GameObject floor;
	// Use this for initialization
	void Start () {
        GetComponent<VRTK_InteractGrab>().ControllerGrabInteractableObject += new ObjectInteractEventHandler(handleGrab);
    }
	
    private void handleGrab(object sender, ObjectInteractEventArgs e)
    {
        AudioSource aud;

        aud = GetComponent<AudioSource>();

        if (floor.activeSelf)
        {
            floor.SetActive(false);
        }
        if (aud != null && !aud.isPlaying)
        {
            aud.Play();
        }
            
    }

	// Update is called once per frame
	void Update () {
		
	}
}
