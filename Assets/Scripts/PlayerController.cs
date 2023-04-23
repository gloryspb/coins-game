using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f; // 
    [SerializeField] private Camera mainCamera; // 

    private bool isMovingWithMouse = false; // 

    public Animator animator;

    private Vector2 moveDirection;

    private void Update()
    {
        //
        if (Input.GetMouseButtonDown(0))
        {
            isMovingWithMouse = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMovingWithMouse = false;
        }

        // 
        if (isMovingWithMouse)
        {
            // 
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            //
            moveDirection = mousePosition - (Vector2)transform.position;

            // 
            if (moveDirection.magnitude > 0.1f)
            {
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else // 
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

                animator.SetFloat("Speed", moveDirection.magnitude);

                animator.SetFloat("Horizontal", horizontalInput);
                animator.SetFloat("Vertical", verticalInput);

                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                
                
            }
            // animator.SetFloat("Horizontal", horizontalInput);
            // animator.SetFloat("Vertical", verticalInput);
            // animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }

        // 

        
    }

}
