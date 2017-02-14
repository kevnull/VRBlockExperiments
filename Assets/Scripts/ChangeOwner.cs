using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOwner : Photon.PunBehaviour {

	public VRTK.VRTK_InteractableObject block;

	// Use this for initialization
	void Start () {
		block.InteractableObjectGrabbed += handleObjectGrabbed;
	}

	void handleObjectGrabbed (object sender, VRTK.InteractableObjectEventArgs e)
	{
		block.GetComponent<PhotonView> ().TransferOwnership (PhotonNetwork.player.ID);
	}
}
