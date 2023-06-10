using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRenderer : MonoBehaviour
{
    public GameObject[] gameObjects = new GameObject[10];
    public Player player;
    void Update()
    {
        foreach (var _gameObject in gameObjects)
        {
            if (int.Parse(_gameObject.name) <= player.HealthPoints)
            {
                ActiveHealth(_gameObject);
            }
            else
            {
                UnctiveHealth(_gameObject);
            }
        }
    }

    void ActiveHealth(GameObject _gameObject)
    {
        _gameObject.transform.Find("active").gameObject.SetActive(true);
        _gameObject.transform.Find("unactive").gameObject.SetActive(false);
    }
    
    void UnctiveHealth(GameObject _gameObject)
    {
        _gameObject.transform.Find("active").gameObject.SetActive(false);
        _gameObject.transform.Find("unactive").gameObject.SetActive(true);
    }
}