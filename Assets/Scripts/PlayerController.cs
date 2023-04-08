using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[serializefield] private float movespeed = 5f; // �������� �������� ���������

    //private rigidbody2d rb; // rigidbody2d ��������� ���������
    //private vector2 movement; // ������ ����������� ��������

    //private void start()
    //{
    //    rb = getcomponent<rigidbody2d>();
    //}

    //private void update()
    //{
    //    // ������������ ���� �� ������
    //    movement.x = input.getaxisraw("horizontal");
    //    movement.y = input.getaxisraw("vertical");
    //}

    //private void fixedupdate()
    //{
    //    // ���������� ��������� � ������������ � �������� ����������� ��������
    //    rb.moveposition(rb.position + movement.normalized * movespeed * time.fixeddeltatime);
    //}

    [SerializeField] private float moveSpeed = 5f; // �������� �������� ���������
    [SerializeField] private Camera mainCamera; // ������ �� ������� ������

    private bool isMovingWithMouse = false; // ������������� ������ �������� ���������

    private void Update()
    {
        // ��������� ������� �� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            isMovingWithMouse = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMovingWithMouse = false;
        }

        // ������� ��������� � ����������� ������� �������, ���� ����� ������ ���� ������
        if (isMovingWithMouse)
        {
            // ��������� ������� ������� �� ������
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // ��������� ����������� �������� ���������
            Vector2 moveDirection = mousePosition - (Vector2)transform.position;

            // ����������� ����������� �������� � ���������� ���������
            if (moveDirection.magnitude > 0.1f)
            {
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else // ���������� ��������� � ������������ � �������� ��������� �� ����������
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
