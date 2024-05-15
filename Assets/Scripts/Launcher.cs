using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Launcher : MonoBehaviour
{
    public GameObject _prefab;
    public bool isShootMode = true;
    public GameObject deleteButton;
    public GameObject microphoneButton;
    public GameObject typeButton;
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0) && isShootMode && !IsTouchOverUI(Input.mousePosition))
#else
        if (Input.touchCount > 0 && isShootMode && !IsTouchOverUI(Input.GetTouch(0).position))
#endif
        {
            //spawn in front of at the camera
            var pos = Camera.main.transform.position;
            var forw = Camera.main.transform.forward;
            var thing = Instantiate(_prefab, pos + (forw * 0.1f), Quaternion.identity, transform);
            isShootMode = false;

            //if it has physics fire it!
            if (thing.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(forw * 100.0f);
            }
        }
    }

    bool IsTouchOverUI(Vector2 touchPosition)
    {
        // Check if the touch is over UI elements
        return EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0);
    }

    public void SummonSuccess(bool success)
    {
        if (success)
        {
            // UI button enable
            deleteButton.SetActive(true);
        }
        else
        {
            isShootMode = true;
        }
    }

    public void RemoveSummon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the child object at index i and destroy it
            Destroy(transform.GetChild(i).gameObject);
        }
        StartCoroutine(DelayedModeChange(1));

        // UI disable
        deleteButton.SetActive(false);
        microphoneButton.SetActive(false);
        typeButton.SetActive(false);
    }

    IEnumerator DelayedModeChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        isShootMode = true;
    }

}