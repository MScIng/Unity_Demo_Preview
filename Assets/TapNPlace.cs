using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapNPlace : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject modelInstance;
    private ARRaycastManager arRaycastManager;
    public bool isSummon = false;

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSummon)
        {
            // Android
            if (Input.touchCount > 0 && !IsTouchOverUI(Input.GetTouch(0).position))
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = touch.position;
                    PlaceObject(touchPosition);
                }
            }
            // Editor
            else if (Input.GetMouseButtonDown(0) && !IsTouchOverUI(Input.mousePosition))
            {
                Vector2 mousePosition = Input.mousePosition;
                PlaceObject(mousePosition);
            }
        }
    }

    void PlaceObject(Vector2 position)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            Vector3 directionToCamera = Camera.main.transform.position - hitPose.position;
            directionToCamera.y = 0; // Keep the direction on the horizontal plane

            // Calculate the rotation that makes the object face the camera
            Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

            // Instantiate the object at the hit position with the calculated rotation
            Instantiate(objectToPlace, hitPose.position, lookRotation, modelInstance.transform);
            isSummon = true;
        }
    }

    bool IsTouchOverUI(Vector2 touchPosition)
    {
        // Check if the touch is over UI elements
        return EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0);
    }
}
