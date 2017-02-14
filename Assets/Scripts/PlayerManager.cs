using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerManager : Photon.PunBehaviour {


	[Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
	public static GameObject LocalPlayerInstance;

	void Awake () {
		// #Important
		// used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
		if ( photonView.isMine)
		{
			PlayerManager.LocalPlayerInstance = this.gameObject;
		}
		// #Critical
		// we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
		DontDestroyOnLoad(this.gameObject);
	}

	void Update () {

        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        transform.position = VRTK_DeviceFinder.HeadsetTransform ().position;
		transform.FindChild("Head").rotation = VRTK_DeviceFinder.HeadsetTransform ().rotation;
	}

}
