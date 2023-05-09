using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; 

    private Animator _animator; 

    private Vector2 _direction;

    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Vector2 _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _direction = (_targetPosition - _rigidbody.position).normalized;
        }
        else
        {
            // Получаем ввод от игрока по осям X и Y
            float _horizontalInput = Input.GetAxisRaw("Horizontal");
            float _verticalInput = Input.GetAxisRaw("Vertical");
            // Создаем вектор направления движения
            _direction = new Vector2(_horizontalInput, _verticalInput).normalized;
        }

        // Тут я решил сменить способ передвижения, ибо первый плохо работает с коллайдерами
        // transform.Translate(_direction * _moveSpeed * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.deltaTime * _moveSpeed);

        _animator.SetFloat("Speed", _direction.magnitude);
        if (_direction != new Vector2(0,0))
        {
            _animator.SetFloat("Vertical", _direction.y);
            _animator.SetFloat("Horizontal", _direction.x);
        }
    }
}
