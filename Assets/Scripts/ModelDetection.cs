using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDetection : MonoBehaviour
{
    public GameObject mircrophoneButton;
    public GameObject typeButton;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "AI Model")
        {
            mircrophoneButton.SetActive(true);
            typeButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AI Model")
        {
            mircrophoneButton.SetActive(false);
            typeButton.SetActive(false);
        }
    }
}
