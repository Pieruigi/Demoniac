using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp
{
    public class GameLauncher : MonoBehaviourPunCallbacks
    {

        public static GameLauncher Instance { get; private set; }

        #region private fields
        string gameVersion;

        // Avoids the player to loop between leave and join room
        bool connecting = false;
        bool playNormalAndVRTogether = false;
        int onlineMaxPlayers = 4;
        #endregion

        #region native methods
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                // Allow the master client to sync scene to other clients
                PhotonNetwork.AutomaticallySyncScene = true;

                // Do we want vr and novr players to play together ?
                string versionVR = "";
                if (!playNormalAndVRTogether && GameManager.Instance.PlayingVR)
                    versionVR = "_vr";
                gameVersion = Application.version + versionVR;
                Debug.Log("Game version: " + gameVersion);

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region public methods
        /// <summary>
        /// Call this method when you want to go online for a coop match.
        /// </summary>
        public void EnterOnlineMode()
        {
            Debug.Log("Entering online mode...");

            // Set online mode
            PhotonNetwork.OfflineMode = false;

            // Connect to master server and join default lobby
            ConnectAndJoinDefaultLobby();
        }

        /// <summary>
        /// Call this method if you want to start offline mode.
        /// </summary>
        public void EnterOfflineMode()
        {
            Debug.Log("Entering offline mode...");

            PhotonNetwork.OfflineMode = true;
        }

        /// <summary>
        /// Quit the online mode disconnecting from photon
        /// </summary>
        public void ExitOnlineMode()
        {
            Debug.Log("Leaving online mode...");
            connecting = false;
            PhotonNetwork.Disconnect();
        }

        /// <summary>
        /// Create a new room that every player can join
        /// </summary>
        public void CreatePublicRoom()
        {
            int maxPlayers = onlineMaxPlayers;


            RoomOptions roomOptions = new RoomOptions() { MaxPlayers = (byte)maxPlayers };
            
            //roomOptions.CustomRoomPropertiesForLobby = new string[] { RoomCustomPropertyKey.PlayerCreator, RoomCustomPropertyKey.MapId };
            roomOptions.IsVisible = true;
            //roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            //roomOptions.CustomRoomProperties.Add(RoomCustomPropertyKey.MatchLength, (int)matchLength);
            //roomOptions.CustomRoomProperties.Add(RoomCustomPropertyKey.PlayerCreator, (string)PhotonNetwork.NickName);
            //roomOptions.CustomRoomProperties.Add(RoomCustomPropertyKey.MapId, (byte)mapId);

            PhotonNetwork.CreateRoom(null, roomOptions);
        }

        public void CreatePrivateRoom()
        {
            int maxPlayers = onlineMaxPlayers;
            RoomOptions roomOptions = new RoomOptions() { MaxPlayers = (byte)maxPlayers };
            // Not visible on the lobby
            roomOptions.IsVisible = false;

            string roomName = "someName";
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        #endregion

        #region pun callbacks
        public override void OnConnectedToMaster()
        {
            if (connecting)
            {
                // This callback gets called even when you leave a game ( moving from game server to 
                // master server ); in that case we don't want to join some room again, so we check
                // the connecting flag.
                connecting = false;
                Debug.LogFormat("PUN - Connected to MasterServer.");
                // Joining or creating room
                Debug.LogFormat("PUN - Joining default lobby...");

                PhotonNetwork.JoinLobby();
            }

        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            Debug.Log("GameLauncher - joined to default lobby");
        }

        public override void OnLeftLobby()
        {
            base.OnLeftLobby();

            Debug.Log("GameLauncher - default lobby left");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            Debug.Log("GameLauncher - OnDisconnected(), cause: " + cause);
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            Debug.Log("GameLauncher OnCreatedRoom() succeeded, room: " + PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);

            Debug.LogFormat("GameLauncher OnCreatedRoom() failed: ({0}) {1}", returnCode, message);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Debug.Log("Local player left room");

        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.LogFormat("Local player joint room: {0}", PhotonNetwork.CurrentRoom);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Connect to the master server and then join the default lobby 
        /// </summary>
        void ConnectAndJoinDefaultLobby()
        {
            connecting = true;

            if (PhotonNetwork.IsConnected)
            {
                // Already connected to photon network, join a random room
                Debug.LogFormat("PUN - Joining default lobby...");

                PhotonNetwork.JoinLobby();
                connecting = false;
            }
            else
            {
                // Connect to the photon network first
                Debug.LogFormat("PUN - Connecting to Photon network...");
                PhotonNetwork.GameVersion = gameVersion;
                Debug.LogFormat("PUN - GameVersion: " + PhotonNetwork.GameVersion);
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        #endregion
    }

}
