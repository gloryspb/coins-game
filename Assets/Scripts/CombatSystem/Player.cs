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
    public void TakeDamage(int damageTaken, Transform enemyTransform)
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
        // Получаем направление от источника урона к игроку
        // Vector2 knockbackDirection = (Vector2)transform.position - (Vector2)enemyTransform.position;
        // knockbackDirection.Normalize();
        // Vector2 knockbackPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance;

        // Применяем отталкивающую силу в противоположном направлении
        // rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        // rb.MovePosition(knockbackPosition);
    }

    void SetColorWhite()
    {
        spriteRenderer.color = Color.white;
    }
    
    public void Death()
    {
        _animator.SetTrigger("DeathTrigger");
        isDead = true;
        // Time.timeScale = 0f;
        // Invoke("DeathHandler", 1f);
        rb.isKinematic = false;
        _collider.enabled = false;
        StartCoroutine(InvokeMethodWithDelay());
    }
	
    // private void DestroyObject()
    // {
    //     // Destroy(gameObject);
    //     Time.timeScale = 1f;
    //     Time.timeScale = 0f;
    // }
    IEnumerator InvokeMethodWithDelay()
    {
        yield return new WaitForSeconds(1f);
        DeathHandler();
    }

    void DeathHandler()
    {
        UIEventHandler.Instance.DeathScreen();
        Time.timeScale = 0f;
        // Debug.Log("death");
    }
}
