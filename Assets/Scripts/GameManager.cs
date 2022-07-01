using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomp
{
    public class GameManager : MonoBehaviour
    {
        #region properties
        public static GameManager Instance { get; private set; }

        public bool PlayingVR
        {
            get { return playingVR; }
        }

        #endregion

        #region private fields
        string vrArg = "-vr";

        bool playingVR = false;
        #endregion

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                // Get arguments
                string[] args = System.Environment.GetCommandLineArgs();
                foreach (string arg in args)
                {
                    if (vrArg.Equals(arg.ToLower()))
                        playingVR = true;
                }

                // Are we forcing to play in vr ?
#if FORCE_VR
            playingVR = true;
#endif
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
    }

}
