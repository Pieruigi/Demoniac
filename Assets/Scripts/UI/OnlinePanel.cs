using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp.UI
{
    public class OnlinePanel : MonoBehaviourPunCallbacks
    {
        #region private
        [SerializeField]
        GameObject connectionPanel;

        bool online = false;
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

        }

        public override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("Online panel enabled");

            // Opening connection panel
            connectionPanel.SetActive(true);

            // Enter online mode
            GameLauncher.Instance.EnterOnlineMode();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            Debug.Log("Online panel disabled");
                        
            if (!online)
                return;

            online = false;
            PhotonNetwork.Disconnect();
        }

        #endregion

        #region public methods
        public void CreatePublicRoom()
        {
            GameLauncher.Instance.CreatePublicRoom();
        }
        #endregion


        #region pun callbacks

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            Debug.Log("Online panel OnConnectedToMaster()");

            
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            Debug.Log("Online panel joined to default lobby");

            // Set online
            online = true;

            // Hide connection panel
            connectionPanel.SetActive(false);
        }

        public override void OnLeftLobby()
        {
            base.OnLeftLobby();

            Debug.Log("Online panel default lobby left");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            online = false;
            Debug.Log("Online panel OnDisconnected(), cause: " + cause);
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            
        }
        #endregion
    }

}
