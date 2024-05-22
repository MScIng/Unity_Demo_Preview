using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBox : MonoBehaviour
{
    public GameObject player;
    public int number;


    void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.summonNum = number;
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Main Scene");
        }
    }
}
