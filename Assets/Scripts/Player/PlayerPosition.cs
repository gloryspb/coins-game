using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] private Text _coordinatesText;
    [SerializeField] private GameObject _player;

    void Update()
    {
        Vector2 position = _player.transform.position;
        _coordinatesText.text = "X: " + Math.Round(position.x, 2) + "; Y: " + Math.Round(position.y, 2);
    }
}
