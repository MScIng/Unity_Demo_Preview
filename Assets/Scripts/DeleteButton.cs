using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    public GameObject deleteButton;
    public GameObject microphoneButton;
    public GameObject typeButton;
    private TapNPlace tapNPlace;

    void Start ()
    {
        tapNPlace = GetComponent<TapNPlace>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tapNPlace.isSummon && !deleteButton.activeInHierarchy)
        {
            deleteButton.SetActive(true);
            deleteButton.GetComponent<Button>().onClick.AddListener(ResetSummon);
        }
    }

    void ResetSummon()
    {
        for (int i = 0; i < tapNPlace.modelInstance.transform.childCount; i++)
        {
            // Get the child object at index i and destroy it
            Destroy(tapNPlace.modelInstance.transform.GetChild(i).gameObject);
        }

        // UI disable
        deleteButton.SetActive(false);
        microphoneButton.SetActive(false);
        typeButton.SetActive(false);
        tapNPlace.isSummon = false;
    }
}
