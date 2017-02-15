using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

public class ClimbOrGrab : VRTK_BaseGrabAttach
{
    private bool grab = false;

    private void Start()
    {
        VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>().AliasGrabOn += new ControllerInteractionEventHandler(handleGrip);
        VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(handleTrigger);
        VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>().AliasGrabOn += new ControllerInteractionEventHandler(handleGrip);
        VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(handleTrigger);
    }

    // Use this for initialization
    protected override void Initialise()
    {
        if (VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>().triggerPressed)
        {
            tracked = false;
            climbable = true;
            kinematic = true;
        }

    }

    private void handleGrip (object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("grip");
    }

    private void handleTrigger(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("trigger ");
    }

}
