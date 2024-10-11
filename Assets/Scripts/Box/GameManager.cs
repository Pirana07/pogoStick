using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int keysCollected = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this GameObject alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    public void CollectKey()
    {
        keysCollected++;
    }

    public int GetKeyCount()
    {
        return keysCollected;
    }
}
