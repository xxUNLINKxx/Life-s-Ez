 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class cubeMovement : MonoBehaviour
{
    [SerializeField] private float speed,jumpForce,jFMultiplier,groundCheckRadius;
    [SerializeField] private int extraJumps; private int jumps;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.AddTorque(0.5f, ForceMode2D.Impulse);
                jumps = extraJumps;
            }
            else if(jumps>0 && rb.velocity.y<=0)
            {
                jumps -= 1;
                rb.AddForce(Vector2.up * jumpForce*jumpMultiplier(), ForceMode2D.Impulse);
            }

        }
    }

    bool isGrounded()
    {
        bool iG;
        iG = Physics2D.OverlapCircle(transform.position, groundCheckRadius, whatIsGround);
        return iG;
    }

    float jumpMultiplier()
    {
        float jM;
        if (rb.velocity.y < 0)
        {
            jM = jFMultiplier;
            rb.velocity = Vector2.zero;
        }
        else
        {
            jM = 1;
        }
        return jM;
    }
}
