using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp.UI
{

    public class GarageMenu : MonoBehaviour
    {
        #region private fields
        [SerializeField]
        GameObject mainPanel;

        [SerializeField]
        GameObject onlinePanel;

        [SerializeField]
        GameObject offlinePanel;


        #endregion

        #region native methods
        private void Awake()
        {
            // Hide all panels
            HideAll();

            // Show the main panel
            mainPanel.SetActive(false);
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

        #region private methods
        void HideAll()
        {
            mainPanel.SetActive(false);
            onlinePanel.SetActive(false);
            offlinePanel.SetActive(false);
        }
        #endregion
    }

}
