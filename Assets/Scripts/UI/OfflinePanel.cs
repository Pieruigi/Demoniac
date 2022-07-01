using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp.UI
{
    public class OfflinePanel : MonoBehaviourPunCallbacks
    {
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

            Debug.Log("Offline panel enabled");
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            Debug.Log("Offline panel disabled");

        }

        #endregion

        #region pun callbacks

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            Debug.Log("Offline panel OnConnectedToMaster()");
        }
        #endregion
    }

}
