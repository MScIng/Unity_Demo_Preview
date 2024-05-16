using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inworld.AEC
{
    public class PushToTalk : AudioCapture
    {
    
        public Button button;
        private bool toggle = false;

        void Start()
        {
            button.onClick.AddListener(Toggling);
        }

        void Update()
        {
            if (toggle)
            {
                StartAudio();
            }
            else
            {
                StopAudio();
            }
        }

        public void Toggling()
        {
            toggle = !toggle;
            Debug.Log(toggle);
        }
    }
}

