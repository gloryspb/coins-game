using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; 
    // [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); 

    private void LateUpdate()
    {
        // Vector3 desiredPosition = target.position + offset;
        // transform.position = desiredPosition;

        transform.position = target.position + new Vector3(0f, 0f, -10f);
    }
}
