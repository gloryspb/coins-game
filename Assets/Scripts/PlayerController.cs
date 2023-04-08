using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[serializefield] private float movespeed = 5f; // скорость движения персонажа

    //private rigidbody2d rb; // rigidbody2d компонент персонажа
    //private vector2 movement; // вектор направления движения

    //private void start()
    //{
    //    rb = getcomponent<rigidbody2d>();
    //}

    //private void update()
    //{
    //    // обрабатываем ввод от игрока
    //    movement.x = input.getaxisraw("horizontal");
    //    movement.y = input.getaxisraw("vertical");
    //}

    //private void fixedupdate()
    //{
    //    // перемещаем персонажа в соответствии с вектором направления движения
    //    rb.moveposition(rb.position + movement.normalized * movespeed * time.fixeddeltatime);
    //}

    [SerializeField] private float moveSpeed = 5f; // скорость движения персонажа
    [SerializeField] private Camera mainCamera; // ссылка на главную камеру

    private bool isMovingWithMouse = false; // переключатель режима движения персонажа

    private void Update()
    {
        // обработка нажатий на левую кнопку мыши
        if (Input.GetMouseButtonDown(0))
        {
            isMovingWithMouse = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMovingWithMouse = false;
        }

        // двигаем персонажа в направлении позиции курсора, если левая кнопка мыши зажата
        if (isMovingWithMouse)
        {
            // вычисляем позицию курсора на экране
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // вычисляем направление движения персонажа
            Vector2 moveDirection = mousePosition - (Vector2)transform.position;

            // нормализуем направление движения и перемещаем персонажа
            if (moveDirection.magnitude > 0.1f)
            {
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else // перемещаем персонажа в соответствии с нажатыми клавишами на клавиатуре
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

}
