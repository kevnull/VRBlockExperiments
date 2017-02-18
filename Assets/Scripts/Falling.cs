using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class Falling : MonoBehaviour {

    private VRTK_HeadsetFade fade;

    private void Awake()
    {
        fade = GetComponent<VRTK_HeadsetFade>();
        fade.HeadsetFadeComplete += handleFadeComplete;
    }
    // Update is called once per frame
    void Update()
    {
        if (VRTK_DeviceFinder.HeadsetTransform().position.y < -100)
        {
            fade.Fade(Color.black, 4f);
        }
    }

    private void handleFadeComplete(object sender, HeadsetFadeEventArgs e)
    {
        GetComponent<Heist.GameManager>().LeaveRoom();
    }
}
