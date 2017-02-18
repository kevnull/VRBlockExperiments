using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlocks : Photon.PunBehaviour {

    public float minSecsPerDrop = 8;
    public Transform[] blockSpawn;
    public GameObject floor;

    private GameObject block;

    private float elapsed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        
        if (elapsed > minSecsPerDrop)
        {
            if (!floor.activeSelf)
                if (PhotonNetwork.connected)
                {
                    PhotonNetwork.InstantiateSceneObject("MBlock", blockSpawn[Mathf.RoundToInt(Random.Range(0, blockSpawn.Length))].position, Quaternion.identity, 0, null);
                }
                else
                {
                    block = (GameObject) Instantiate(Resources.Load("MBlock"));
                    block.transform.position = blockSpawn[Mathf.RoundToInt(Random.Range(0, blockSpawn.Length))].position;
                }
            elapsed = 0;
        }
	}
}
