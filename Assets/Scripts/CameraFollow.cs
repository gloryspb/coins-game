using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // ����, �� ������� ����� ��������� ������
    [SerializeField] private float smoothing = 5f; // ����������� ��� �������� ���������� ������ �� �����
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // �������� ������ ������������ ����

    private void LateUpdate()
    {
        // ��������� ����� ������� ������
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);

        // ���������� ������
        transform.position = smoothedPosition;
    }
}
