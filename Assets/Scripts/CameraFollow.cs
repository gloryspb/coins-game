using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // цель, за которой будет следовать камера
    [SerializeField] private float smoothing = 5f; // сглаживание для плавного следования камеры за целью
    [SerializeField] private Vector2 offset = new Vector2(0f, 0f, -10f); // смещение камеры относительно цели

    private void LateUpdate()
    {
        // вычисляем новую позицию камеры
        Vector2 desiredPosition = target.position + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);

        // перемещаем камеру
        transform.position = smoothedPosition;
    }
}
