using Inworld.Assets;
using UnityEngine;
using UnityEngine.UI;


namespace Inworld.Sample.RPM
{
    public class PlayerControllerT5 : PlayerController3D
    {
        FeedbackCanvas m_FeedbackDlg;
        public Button chatToggleButton1;
        public Button chatToggleButton2;
        protected void Awake()
        {
            m_FeedbackDlg = m_FeedbackCanvas.GetComponent<FeedbackCanvas>();
            // Ensure the button is assigned
            if (chatToggleButton1 != null && chatToggleButton2 != null)
            {
                chatToggleButton1.onClick.AddListener(ToggleChatCanvas);
                chatToggleButton2.onClick.AddListener(ToggleChatCanvas);
            }
        }
        protected override void HandleCanvas()
        {
            bool anyCanvasOpen = m_ChatCanvas && m_ChatCanvas.activeSelf ||
                                 m_StatusCanvas && m_StatusCanvas.activeSelf ||
                                 m_FeedbackCanvas && m_FeedbackCanvas.activeSelf ||
                                 m_OptionCanvas && m_OptionCanvas.activeSelf;
            // m_CameraController.enabled = !anyCanvasOpen;
        }
        private void ToggleChatCanvas()
        {
            if (m_ChatCanvas != null)
            {
                m_ChatCanvas.SetActive(!m_ChatCanvas.activeSelf);
                HandleCanvas(); // Update canvas handling based on the new state
            }
        }
        public override void OpenFeedback(string interactionID, string correlationID)
        {
            m_FeedbackDlg.Open(interactionID, correlationID);
        }
    }
}
