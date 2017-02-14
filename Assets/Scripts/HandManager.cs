using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandManager : Photon.PunBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update () {

        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        if (name.Contains("LHand"))
        {
            pos = VRTK_DeviceFinder.GetControllerLeftHand(true).transform.position;
            rot = VRTK_DeviceFinder.GetControllerLeftHand(true).transform.rotation;
        }
        else if (name.Contains("RHand"))
        {
            pos = VRTK_DeviceFinder.GetControllerRightHand(true).transform.position;
            rot = VRTK_DeviceFinder.GetControllerRightHand(true).transform.rotation;
        }
        transform.position = pos;
        transform.rotation = rot;
	}

}
