using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour {

    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
        AudioSource aud;
        aud = GetComponent<AudioSource>();
        if (!aud.isPlaying)
            aud.Play();
    }
}
