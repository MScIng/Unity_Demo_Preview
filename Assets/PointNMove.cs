using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PointNMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    // public PlayerLocationController controller;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // unity editor mode
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }

        // phone mode
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleInput(Input.GetTouch(0).position);
        }
    }

    void HandleInput(Vector3 inputPosition)
    {
       Ray ray = Camera.main.ScreenPointToRay(inputPosition);
       RaycastHit hitPoint;

       if (Physics.Raycast(ray, out hitPoint))
       {
        // check what object did ray hit
        if (hitPoint.collider.gameObject.name == "ModelA_map")
        {
            Debug.Log("done");
            // PlayerLocationController controller = GetComponent<PlayerLocationController>();
            // controller.setActive(false);
            ;
            StartCoroutine(MoveRight());
            
        }
        else if (hitPoint.collider.gameObject.name == "ModelB_map")
        {
            Debug.Log("done");
            // PlayerLocationController controller = GetComponent<PlayerLocationController>();
            // controller.setActive(false);
            ;
            StartCoroutine(MoveLeft());
            
        }
       }

    }

    IEnumerator MoveRight()
    {
        for (int i = 0; i < 50; i++)
        {
            player.transform.position += Vector3.right * 0.5f ;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator MoveLeft()
    {
        for (int i = 0; i < 50; i++)
        {
            player.transform.position += Vector3.left * 0.5f ;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
