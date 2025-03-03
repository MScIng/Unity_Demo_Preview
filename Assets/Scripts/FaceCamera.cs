using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public bool freezeRotationX;
    public bool freezeRotationY;
    public bool freezeRotationZ;

    void Update()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}
