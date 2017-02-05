﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;


namespace Com.MyCompany.MyGame
{
    public class GameManager : Photon.PunBehaviour
    {

		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;

        #region Photon Messages


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


		public void Start()
		{
			if (PlayerManager.LocalPlayerInstance==null)
			{
				Debug.Log("We are Instantiating LocalPlayer from "+Application.loadedLevelName);
				// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
				GameObject playergo = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
				playergo.transform.position = VRTK_DeviceFinder.HeadsetTransform ().position;
				playergo.transform.rotation = VRTK_DeviceFinder.HeadsetTransform ().rotation;
                PhotonNetwork.Instantiate("Inventory", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Hand", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            }
            else
            {
				Debug.Log("Ignoring scene load for "+Application.loadedLevelName);
			}
		}

        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount);
            PhotonNetwork.LoadLevel("Room");
        }


        #endregion

        #region Photon Messages


        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting


            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


                LoadArena();
            }
        }


        public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects


            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


                LoadArena();
            }
        }


        #endregion
    }
}