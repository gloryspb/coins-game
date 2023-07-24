using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else //if (instance == this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
        InitializeManager();
    }

    private void InitializeManager()
    {
        
    }
}
