using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float healthPoints = 10f;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void TakeDamage(float damageTaken)
    {
        healthPoints -= damageTaken;
        if (healthPoints <= 0f)
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
