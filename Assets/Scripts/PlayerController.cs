using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f; // Скорость перемещения персонажа

    private Animator animator; // Ссылка на компонент Animator

    private void Awake()
    {
        // Получаем ссылку на компонент Animator
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Получаем ввод от игрока по осям X и Y
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Создаем вектор направления движения
        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;

        // Если направление не равно нулю, перемещаем персонажа в заданном направлении
        if (direction.magnitude > 0f)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Устанавливаем значения переменных аниматора в зависимости от направления движения персонажа
            animator.SetFloat("Speed", 1f);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Horizontal", direction.x);
        }
        else
        {
            // Если персонаж стоит на месте, останавливаем анимацию ходьбы
            animator.SetFloat("Speed", 0f);
        }
    }
}
