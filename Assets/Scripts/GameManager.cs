using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;


namespace Heist
{
    public class GameManager : Photon.PunBehaviour
    {

		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;
        public GameObject lhandPrefab;
        public GameObject rhandPrefab;
        public Transform[] spawnPoints;

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
                
                if (PhotonNetwork.connected)
                {
                    GameObject playergo = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoints[PhotonNetwork.playerList.Length - 1].position, Quaternion.identity, 0);
                    PhotonNetwork.Instantiate(this.lhandPrefab.name, VRTK_DeviceFinder.GetControllerLeftHand(true).transform.position, VRTK_DeviceFinder.GetControllerLeftHand(true).transform.rotation, 0);
                    PhotonNetwork.Instantiate(this.rhandPrefab.name, VRTK_DeviceFinder.GetControllerRightHand(true).transform.position, VRTK_DeviceFinder.GetControllerRightHand(true).transform.rotation, 0);
                }
                VRTK_DeviceFinder.PlayAreaTransform().position = spawnPoints[PhotonNetwork.playerList.Length - 1].position;
            }
            else
            {
				Debug.Log("Ignoring scene load for "+Application.loadedLevelName);
			}
            var temp = PhotonVoiceNetwork.Client;
		}

        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion


        #region Photon Messages


        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting


            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
            }
        }


        public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects


            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
            }
        }

        private void OnDestroy()
        {

        }

        #endregion
    }
}