using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaManager : MonoBehaviour {

    // Play sounds whenever collision happens
    private void OnCollisionEnter(Collision collision)
    {
        AudioSource aud;
        aud = GetComponent<AudioSource>();
        if (!aud.isPlaying)
            aud.Play();
    }

	// Destroy if it falls past a certain height
	void Update ()
	{
		if (transform.position.y < -150f) {
			Destroy (this.gameObject);
		}
	}
}
