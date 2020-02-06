using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReset : MonoBehaviour
{ 
    void Awake()
    {
        ClearSingleton();
        SetUpSingleton();
    }

    public void ClearSingleton()
    {
        var music = FindObjectsOfType(GetType());

        foreach (GameObject i in music)
        {
            Destroy(i);
        }
    }
    public void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
