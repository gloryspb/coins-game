using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; // Скорость перемещения персонажа

    private Animator _animator; // Ссылка на компонент Animator

    private Vector2 _direction;

    private void Awake()
    {
        // Получаем ссылку на компонент Animator
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            // Получаем координаты курсора, создаем вектор направления в зависимости от его положения
            Vector2 _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _direction = (_targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
        else
        {
            // Получаем ввод от игрока по осям X и Y
            float _horizontalInput = Input.GetAxisRaw("Horizontal");
            float _verticalInput = Input.GetAxisRaw("Vertical");
            // Создаем вектор направления движения
            _direction = new Vector2(_horizontalInput, _verticalInput).normalized;
        }

        transform.Translate(_direction * _moveSpeed * Time.deltaTime);

        _animator.SetFloat("Speed", _direction.magnitude);
        _animator.SetFloat("Vertical", _direction.y);
        _animator.SetFloat("Horizontal", _direction.x);
    }
}
