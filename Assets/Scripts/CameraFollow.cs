using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // ����, �� ������� ����� ��������� ������
    [SerializeField] private float smoothing = 5f; // ����������� ��� �������� ���������� ������ �� �����
    [SerializeField] private Vector2 offset = new Vector2(0f, 0f, -10f); // �������� ������ ������������ ����

    private void LateUpdate()
    {
        // ��������� ����� ������� ������
        Vector2 desiredPosition = target.position + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);

        // ���������� ������
        transform.position = smoothedPosition;
    }
}
