using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject component;
    public bool freezeRotationX;
    public bool freezeRotationY;
    public bool freezeRotationZ;

    void Update()
    {
        Vector3 direction = target.transform.position - component.transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        component.transform.rotation = rotation;
    }
}
