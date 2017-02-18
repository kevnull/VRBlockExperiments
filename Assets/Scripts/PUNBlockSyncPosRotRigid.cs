using UnityEngine;
using System.Collections;
using Photon;

public class PUNBlockSyncPosRotRigid : Photon.PunBehaviour
{

    [Header("Options")]
    public float posSmoothSpeed = 10f;
    public float rotSmoothSpeed = 10f;
    public float snapToDistance = 2.0f;

    private Transform myTransform;
    private Vector3 mostRecentPos;
    //private Vector3 prevPos;

    private Quaternion mostRecentRot;
    //private Quaternion prevRot;

    void Awake()
    {
        myTransform = transform;

        mostRecentPos = transform.position;
        mostRecentRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            if (myTransform.position != mostRecentPos)
            {
                if (Vector3.Distance(mostRecentPos, myTransform.position) >= snapToDistance)
                {
                    myTransform.position = mostRecentPos;
                }
                else
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, mostRecentPos, Time.deltaTime * posSmoothSpeed);
                }
            }

            if( myTransform.rotation != mostRecentRot )
            {
                myTransform.rotation = Quaternion.Lerp(myTransform.rotation, mostRecentRot, Time.deltaTime * rotSmoothSpeed);
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this Block: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network block, receive data
            mostRecentPos = (Vector3)stream.ReceiveNext();
            mostRecentRot = (Quaternion)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void RpcSetPositionRotation(Vector3 position, Quaternion rotation)
    {
        myTransform.position = position;
        myTransform.rotation = rotation;
        mostRecentPos = position;
        mostRecentRot = rotation;
    }
}