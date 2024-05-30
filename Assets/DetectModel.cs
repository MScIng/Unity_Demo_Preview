using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectModel : MonoBehaviour
{

    public string tagName;
    public GameObject[] buttons;
    public List<GameObject> inRange = new List<GameObject>();

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == tagName)
        {
            foreach (var button in buttons)
            {
                button.SetActive(true);
                inRange.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (var button in buttons)
        {
            inRange.Remove(other.gameObject);
            if (inRange.Count == 0)
            {
                button.SetActive(false);
            }

        }
    }

    public void SelectClosest(string sceneName)
    {
        GameObject closest = inRange[0];
        foreach (var obj in inRange)
        {
            if (Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = obj;
            }
        }
        Debug.Log(closest.name);
        ModelProperties properties = closest.GetComponent<ModelProperties>();
        GameManager.Instance.summonNum = properties.type;
        SceneManager.LoadScene(sceneName);

    }
}
