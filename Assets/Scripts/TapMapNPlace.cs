using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapMapNPlace : TapNPlace
{
    public int number = 0;

    public override void PlaceObject(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && GameManager.Instance.mapSummon < objectToPlace.Length)
        {
            if (hit.collider.gameObject.name == "Plane")
            {
                Vector3 hitPosition = hit.point;
                hitPosition.y += 1;
                Instantiate(objectToPlace[GameManager.Instance.mapSummon], hitPosition, Quaternion.identity , modelInstance.transform);
                GameManager.Instance.mapSummon++;

            }
        }
        // else
        // {
        //     Debug.Log("No GameObject hit.");
        // }
    }
}
