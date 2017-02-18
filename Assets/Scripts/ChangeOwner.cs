using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOwner : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectGrabbed += handleObjectGrabbed;
	}

	void handleObjectGrabbed (object sender, VRTK.InteractableObjectEventArgs e)
	{
        if (PhotonNetwork.connected)
        {
            GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        }
    }
}
