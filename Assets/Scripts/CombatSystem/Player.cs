using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Damage;
    public int HealthPoints;
    private Animator _animator;
    public float knockbackForce = 50f;
    private Rigidbody2D rb;
    public float knockbackDistance = 2f;
    private SpriteRenderer spriteRenderer;
    public static bool isDead;
    private Collider2D _collider;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        HealthPoints = 10;
        isDead = false;
    }
    public void TakeDamage(int damageTaken)
    {
        if (PlayerController.isAttack)
        {
            return;
        }
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
        isDead = true;
        rb.isKinematic = false;
        _collider.enabled = false;
        StartCoroutine(InvokeMethodWithDelay());
    }

    IEnumerator InvokeMethodWithDelay()
    {
        yield return new WaitForSeconds(1f);
        DeathHandler();
    }

    void DeathHandler()
    {
        UIEventHandler.Instance.DeathScreen();
        Time.timeScale = 0f;
    }
}
