using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneTrigger : MonoBehaviour
{
    private EnemyController _enemyController;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemyController = other.gameObject.GetComponent<EnemyController>();
            _enemyController.Death();
        }
    }
}
