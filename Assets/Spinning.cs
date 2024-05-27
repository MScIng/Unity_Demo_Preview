using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // Rotation speed in degrees per second

    void Update() {
        // Rotate the GameObject
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
