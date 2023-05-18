using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    public Text coordinatesText;
    public GameObject _player;

    void Update()
    {
        Vector2 position = _player.transform.position;
        coordinatesText.text = "X: " + Math.Round(position.x, 2) + "; Y: " + Math.Round(position.y, 2);
    }
}
