using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private void LateUpdate()
    {
        transform.position = _target.position + new Vector3(0f, 0f, -10f);
    }
}