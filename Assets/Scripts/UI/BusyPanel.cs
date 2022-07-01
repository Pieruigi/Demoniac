using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Zomp.UI
{
    public class BusyPanel : MonoBehaviour
    {
        [SerializeField]
        TMP_Text busyText;

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

        public void Show(string text)
        {
            busyText.text = text;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }

}
