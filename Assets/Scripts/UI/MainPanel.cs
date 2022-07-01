using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp.UI
{
    public class MainPanel : MonoBehaviourPunCallbacks
    {
        #region private fields
        [SerializeField]
        GameObject onlinePanel;

        [SerializeField]
        GameObject offlinePanel;

        [SerializeField]
        BusyPanel busyPanel;
        #endregion

        #region native methods
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
        /// Entering online mode
        /// </summary>
        public void EnterOnlineMode()
        {
            // Opening connection panel
            busyPanel.Show("Connecting to server...");

            // Enter online mode
            GameLauncher.Instance.EnterOnlineMode();
        }
        #endregion


        #region pun callbacks
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            Debug.Log("Main panel OnConnectedToMaster()");

            // Hide busy panel
            busyPanel.Hide();

            // Show online panel
            onlinePanel.SetActive(true);

            // Hide this panel
            gameObject.SetActive(false);
        }


        
        

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            // Hide the busy panel if needed
            busyPanel.Hide();

            Debug.Log("Main panel OnDisconnected(), cause: " + cause);
        }
        #endregion
    }

}
