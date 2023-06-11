using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageZoneTrigger : MonoBehaviour
{
    private Player _player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _player = gameObject.GetComponentInParent<Player>();
            _player.TakeDamage(1, other.gameObject.transform);
            // Debug.Log(Player.HealthPoints);
        }
    }
}