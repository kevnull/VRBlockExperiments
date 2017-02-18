using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class OpenMenu : MonoBehaviour {

    public GameObject ui;

	// Use this for initialization
	void Awake () {
        GetComponent<VRTK_ControllerEvents>().AliasMenuOn += handleMenuOn;
        GetComponent<VRTK_ControllerEvents>().AliasMenuOff += handleMenuOff;
    }

    void handleMenuOn (object sender, ControllerInteractionEventArgs e)
    {
        ui.SetActive(!ui.activeSelf);
	}
    void handleMenuOff(object sender, ControllerInteractionEventArgs e)
    {
    }
}
