using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Niantic.Lightship.Maps.Core.Coordinates;
using UnityEngine.SceneManagement;





public class PointNMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float movementSpeed = 5f;
    public int number = 0;
    private bool moving = false;
    private Vector3 targetDirection = Vector3.zero;
    //public Niantic.Lightship.Maps.SampleAssets.Player.PlayerLocationController controller;
    // public PlayerLocationController controller;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //controller.enabled = false;
    }

    void Update()
    {
        // Android
        if (Input.touchCount > 0 && !IsTouchOverUI(Input.GetTouch(0).position))
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;
                RayCheck(touchPosition);
            }
        }
        // Editor
        else if (Input.GetMouseButtonDown(0) && !IsTouchOverUI(Input.mousePosition))
        {
            Vector2 mousePosition = Input.mousePosition;
            RayCheck(mousePosition);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 6f && moving)
        {
            moving = false;
            // EnterAR();
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            MoveToMe(targetDirection);
        }
    }
    void RayCheck(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                
                Vector3 direction = transform.position - player.transform.position;
                direction.y = 0;
                moving = true;
                targetDirection = direction.normalized;
            }
        }
    }

    public void MoveToMe(Vector3 step)
    {
        // Rigidbody rb = player.GetComponent<Rigidbody>();
        player.transform.position += step * movementSpeed * Time.deltaTime;
        

    }

    void EnterAR()
    {
        GameManager.Instance.summonNum = number;
        Destroy(gameObject);
        SceneManager.LoadScene("Main Scene");
    }

    protected virtual bool IsTouchOverUI(Vector2 touchPosition)
    {
        // Check if the touch is over UI elements
        return EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0);
    }

}
