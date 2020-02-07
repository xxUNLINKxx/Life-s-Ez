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
    [SerializeField] private SpriteRenderer[] jumpUi;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (isGrounded())
        {                
            jumps = extraJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.AddTorque(0.5f, ForceMode2D.Impulse);
            }
            else if(jumps>0 && rb.velocity.y<=0.5f)
            {
                jumps -= 1;
                rb.AddForce(Vector2.up * jumpForce*jumpMultiplier(), ForceMode2D.Impulse);
            }

        }

        for(int i = 0; i < jumpUi.Length; i++)
        {
            if (i < jumps)
            {
                jumpUi[i].color = Color.white;
            }
            else
            {
                jumpUi[i].color = Color.gray;
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
