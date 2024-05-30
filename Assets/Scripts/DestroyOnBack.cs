using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBack : MonoBehaviour
{
    public GameObject item;

    public void RemoveItem()
    {
        Destroy(item);
    }
}
