using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp.UI
{
    public class LobbyPanel : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        GameObject onlinePanel;

        [SerializeField]
        GameObject offlinePanel;

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
        #endregion


        #region public methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion

        #region pun callbacks

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            gameObject.SetActive(false);

            if (PhotonNetwork.OfflineMode)
                offlinePanel.SetActive(true);
            else
                onlinePanel.SetActive(true);
        }
        #endregion
    }

}
