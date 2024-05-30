using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPSOptimizer : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Start()
    {
        player = GameObject.Find("Player"); ;
        StartCoroutine(RunEveryTenSeconds());
    }
    IEnumerator RunEveryTenSeconds()
    {
        while (true)
        {
            // Your task here
            if (Vector3.Distance(transform.position, player.transform.position) > 1000)
            {
                Destroy(gameObject);
            }

            // Wait for 10 seconds
            yield return new WaitForSeconds(10f);
        }
    }
}
