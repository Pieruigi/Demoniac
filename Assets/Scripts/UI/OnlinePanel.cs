using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp.UI
{
    public class OnlinePanel : MonoBehaviourPunCallbacks
    {
        #region private
        [SerializeField]
        BusyPanel busyPanel;

        [SerializeField]
        GameObject lobbyPanel;

        [SerializeField]
        GameObject mainPanel;

        //DateTime lastJoinLobbyAttempt;
        #endregion

        #region native methods
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //if (!PhotonNetwork.InLobby)
            //{
            //    if ((DateTime.UtcNow - lastJoinLobbyAttempt).TotalSeconds > 1)
            //    {
            //        lastJoinLobbyAttempt = DateTime.UtcNow;
            //        PhotonNetwork.JoinLobby();
            //    }
                    
            //}
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("Online panel enabled");

            // Coming from the main panel
            PhotonNetwork.JoinLobby();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            Debug.Log("Online panel disabled");
                        
           
        }

        #endregion

        #region public methods
        public void CreatePublicRoom()
        {
            // Create a public room
            GameLauncher.Instance.CreatePublicRoom();

            // Show busy panel
            busyPanel.Show("Creating room...");
        }

        public void DisconnectFromPhoton()
        {
            PhotonNetwork.Disconnect();

            
        }
        #endregion


        #region pun callbacks
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            Debug.Log("Online panel OnConnectedToMaster()");

            // Going back from the lobby panel
            PhotonNetwork.JoinLobby();
        }



        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            Debug.Log("Online panel joined to default lobby");


        }

        public override void OnLeftLobby()
        {
            base.OnLeftLobby();

            Debug.Log("Online panel default lobby left");
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            // Hide busy panel
            busyPanel.Hide();

            // Show lobby panel
            lobbyPanel.SetActive(true);

            // Hide this panel
            gameObject.SetActive(false);
            
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);

            // Hide busy panel
            busyPanel.Hide();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            // Rejoin the default lobby
            
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            // Hide busy panel
            busyPanel.Hide();

            if(cause != DisconnectCause.DisconnectByClientLogic)
            {
                // Show some error message here
            }

            // Connection problem 
            // Reload the main panel
            gameObject.SetActive(false);
            mainPanel.SetActive(true);
        }
        #endregion
    }

}
