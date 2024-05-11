using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObj : MonoBehaviour
{
    private bool hit = false;
    private bool onGround = false;
    public GameObject summonModel;
    
    private Launcher launcher;

    void Start()
    {
        Invoke("OutOfBound", 3.0f);
        GameObject launcherScript = transform.parent.gameObject;
        launcher = launcherScript.GetComponent<Launcher>();
    }
    void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        if (!hit)
        {
            Invoke("Summon", 1.0f);
            print(collision.transform);
            hit = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
    void Summon()
    {
        Destroy(gameObject);
        if (onGround)
        {
            Vector3 direction = Camera.main.transform.position - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            Instantiate(summonModel, gameObject.transform.position, rotation, transform.parent);
            launcher.SummonSuccess(true);
        }
        else
        {
            launcher.SummonSuccess(false);
            Debug.Log("Not on Ground");
        }
        
    }


    void OutOfBound()
    {
        Destroy(gameObject);
        launcher.SummonSuccess(false);
        Debug.Log("Out Of Bound, Scan Before You Summon.");
    }

}
