using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    private Animator _animator;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private float _timer;
    bool isTrue;
	private Transform player;
	public float detectionRadius = 10f;
	public bool haveAnimation;
	private SpriteRenderer spriteRenderer;
	public int HealthPoints = 5;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        isTrue = false;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
		float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Если игрок в радиусе обнаружения, двигаемся в его сторону
            _direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
			if (isTrue)
			{
				_direction.x *= -1;
				_direction.y *= -1;
			}
            _rigidbody.velocity = _direction * _moveSpeed;
 			_animator.SetFloat("Speed", _direction.magnitude);
        }
        else
        {
            // Если игрок не в радиусе обнаружения или не активен, останавливаемся
            _rigidbody.velocity = Vector2.zero;
 			_animator.SetFloat("Speed", 0f);
        }

        if (_direction != new Vector2(0, 0) && !haveAnimation)
        {
            Vector2 Scaler = transform.localScale;
            if (_direction.x < 0 && Scaler.x > 0)
            {
                Scaler.x *= -1;
            }
            else if (_direction.x > 0 && Scaler.x < 0)
            {
                Scaler.x *= -1;
            }
            transform.localScale = Scaler;
        }
		if (haveAnimation)
		{
			_animator.SetFloat("Vertical", _direction.y);
			_animator.SetFloat("Horizontal", _direction.x);
		}
    }
	public void TakeDamage(int damageTaken)
    {
        HealthPoints -= damageTaken;
        if (HealthPoints <= 0)
        {
            Death();
        }
		
		// Debug.Log(HealthPoints);

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
		Invoke("DestroyObject", 0.83f);
	}
	
	private void DestroyObject()
	{
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrue = true;
			Invoke("SetFalse", 0.5f);
        }
    }

	private void SetFalse()
	{
		isTrue = false;
	}
}
