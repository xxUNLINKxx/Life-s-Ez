using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class cubeMovement : MonoBehaviour
{
    [SerializeField] private float speed,jumpForce,groundCheckRadius;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool isGrounded()
    {
        bool iG;
        iG = Physics2D.OverlapCircle(transform.position, groundCheckRadius, whatIsGround);
        return iG;
    }
}
