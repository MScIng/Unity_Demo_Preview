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
        private bool active = false;

        void Start()
        {
            button.onClick.AddListener(Toggling);
        }

        void Update()
        {
            
        }

        public void Toggling()
        {
            toggle = !toggle;
            if (toggle)
            {
                StartAudio();
            }
            else
            {
                StopAudio();
            }
            Debug.Log(toggle);
        }
    }
}

