using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChild : MonoBehaviour
{
    // Start is called before the first frame update
    private Launcher launcher;
    void Start()
    {
        launcher = GetComponent<Launcher>();
    }
    public void RemoveChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the child object at index i and destroy it
            Destroy(transform.GetChild(i).gameObject);
        }
        launcher.isShootMode = true;
    }
}
