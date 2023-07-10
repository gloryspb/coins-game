using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int HealthPoints = 10;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void TakeDamage(int damageTaken)
    {
        HealthPoints -= damageTaken;
        if (HealthPoints <= 0)
        {
            Death();
        }

        spriteRenderer.color = Color.red;
        Invoke("SetColorWhite", 0.25f);
    }
	
    void SetColorWhite()
    {
        spriteRenderer.color = Color.white;
    }

    public void Death()
    {
        _animator.SetTrigger("DeathTrigger");
        Invoke("DestroyObject", 1.3f);
    }
	
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
