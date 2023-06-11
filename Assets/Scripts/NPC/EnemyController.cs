using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Animator _animator;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private float _timer;
    bool isTrue;
	private Transform player;
	public float detectionRadius = 10f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        isTrue = false;

		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //_direction = Vector2.zero;
        //_timer += Time.deltaTime;
        //if (_timer > 2)
        //{
        //    if (isTrue)
        //    {
        //        x *= -1;
        //    }
        //    else
        //    {
        //        y *= -1;
        //    }
        //    _timer = 0;
        //    isTrue = !isTrue;
        //}

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

        //_direction = new Vector2(x, y);
        //_direction.Normalize();
        //_rigidbody.MovePosition(_rigidbody.position + _direction * Time.deltaTime * _moveSpeed);

        //_animator.SetFloat("Speed", _direction.magnitude);


        if (_direction != new Vector2(0, 0))
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
