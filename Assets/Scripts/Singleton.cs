using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;
    public int summonNum = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            summonNum = Instance.summonNum;
            Destroy(Instance.gameObject);
            // return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
