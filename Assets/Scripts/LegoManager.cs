using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LegoManager : MonoBehaviour {

    public Transform[] bridgeSlots;

    private int[] bridgeCreated;
    private int pid;
    private bool gameOver;

	// Use this for initialization
	void Awake () {
        VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(handleTouchpad);
        VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(handleTouchpad);

        bridgeCreated = new int[bridgeSlots.Length];
        for (int i = 0; i < bridgeSlots.Length; i++)
        {
            bridgeCreated[i] = 0;
        }

        pid = PhotonNetwork.player.ID;
    }

    // Update is called once per frame
    void handleTouchpad (object sender, ControllerInteractionEventArgs e) {
        float a = e.touchpadAngle;
        float tolerance = 20f;
        if (a > 360f-tolerance || a < tolerance)
        {
            Debug.Log("up");
        }
        else if (a > 90f-tolerance && a < 90f+tolerance)
        {
            Debug.Log("right");

        }
        else if (a > 180f-tolerance && a < 180f+tolerance)
        {
            Debug.Log(pid + " pressed down with controller " + e.controllerIndex);
            GameObject go;

            if (!gameOver)
            {
                makeBridge(pid);
            } else
            {
                Debug.Log("reset bridge here");
            }
        }
        else if (a > 270f-tolerance && a < 270f+tolerance)
        {
            Debug.Log("left");
        }

    }

    private void makeBridge(int id)
    {
        id = 2;
        int i = 0;
        GameObject go;
        Transform b;

        if (id == 1)
        {
            i = 0;
            while (i < bridgeCreated.Length && bridgeCreated[i] > 0)
                i++;
        }
        else
        {
            i = bridgeCreated.Length - 1;
            while (i >= 0 && bridgeCreated[i] > 0)
                i--;
        }

        bridgeCreated[i] = id;
        b = bridgeSlots[i];

        if (PhotonNetwork.connected)
        {
            go = PhotonNetwork.Instantiate("Bridge", b.position, b.rotation, 0);
        }
        else
        {
            go = (GameObject)Instantiate(Resources.Load("Bridge"));
            go.transform.position = b.position;
            go.transform.rotation = b.rotation;
        }

        go.GetComponent<Renderer>().material.color = (id == 1) ? Color.blue : Color.red;

        bool emptyslots = false;
        for (i = 0; i < bridgeCreated.Length; i++)
            if (bridgeCreated[i] == 0)
                emptyslots = true;
        if (!emptyslots)
        {
            gameOver = true;
            Debug.Log("game over!");
        }
    }
}
