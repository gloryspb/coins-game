using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneTrigger : MonoBehaviour
{
    private EnemyController _enemyController;
	private BossController _bossController;	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemyController = other.gameObject.GetComponent<EnemyController>();
            _enemyController.TakeDamage(1);
        }

		if (other.gameObject.tag == "Boss")
        {
            _bossController = other.gameObject.GetComponent<BossController>();
            _bossController.TakeDamage(1);
        }
    }
}
