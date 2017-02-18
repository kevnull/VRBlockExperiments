using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Hands : MonoBehaviour {

    public GameObject lhandPrefab;
    public GameObject rhandPrefab;
    private GameObject lh;
    private GameObject rh;
    // Use this for initialization
    void Start () {
        lh = (GameObject) Instantiate(Resources.Load(this.lhandPrefab.name));
        rh = (GameObject)Instantiate(Resources.Load(this.rhandPrefab.name));
    }

    // Update is called once per frame
    void Update () {
        lh.transform.position = VRTK_DeviceFinder.GetControllerLeftHand(true).transform.position;
        lh.transform.rotation = VRTK_DeviceFinder.GetControllerLeftHand(true).transform.rotation;
        rh.transform.position = VRTK_DeviceFinder.GetControllerRightHand(true).transform.position;
        rh.transform.rotation = VRTK_DeviceFinder.GetControllerRightHand(true).transform.rotation;
    }
}
