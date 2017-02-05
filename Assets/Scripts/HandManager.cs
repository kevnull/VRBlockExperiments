using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandManager : Photon.PunBehaviour
{


    void Awake()
    {
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        transform.position = VRTK_DeviceFinder.GetControllerLeftHand().transform.position;
        transform.rotation = VRTK_DeviceFinder.GetControllerLeftHand().transform.rotation;
    }

}
