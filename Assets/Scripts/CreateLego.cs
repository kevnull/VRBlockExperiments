using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CreateLego : Photon.PunBehaviour {

    public GameObject blockPrefab;

	// Use this for initialization
	void Awake () {
        GetComponent<VRTK_ControllerEvents>().AliasUseOn += new ControllerInteractionEventHandler(createBlock);
	}
	
	// Update is called once per frame
	void createBlock (object sender, ControllerInteractionEventArgs e) {

        GameObject go;

        if (PhotonNetwork.connected)
        {
            go = PhotonNetwork.Instantiate(blockPrefab.name, Vector3.zero, Quaternion.identity, 0);
        }
        else
        {
            go = (GameObject) Instantiate(Resources.Load(blockPrefab.name));
        }

        VRTK_ObjectAutoGrab autograb = GetComponent<VRTK_ObjectAutoGrab>();

        autograb.objectToGrab = go.GetComponent<VRTK_InteractableObject>();
        autograb.enabled = true;
        autograb.enabled = false;

    }
}
