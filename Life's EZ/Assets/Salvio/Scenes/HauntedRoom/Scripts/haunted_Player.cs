using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class haunted_Player : MonoBehaviour
{

    [SerializeField] private float speed, jumpForce, pushForce, groundCheckRadius, grabRadius;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask whatIsGround,whatIsGrab;
    [SerializeField] private Vector2[] limits;
    private bool facingRight = true, hasGrabbed = false;
    private GameObject grabbedObj;
    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        traverse();
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (Mathf.Abs(moveInput) > 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (facingRight && moveInput < 0|| !facingRight && moveInput>0)
        {
            Flip();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canGrab())
            {
                hasGrabbed = true;
            }
            if(grabbedObj != null)
            {
                hasGrabbed = false;
            }

        }
        Grab();
    }
    bool isGrounded()
    {
        bool iG;
        iG = Physics2D.OverlapCircle(transform.GetChild(1).position, groundCheckRadius, whatIsGround);
        return iG;
    }

    int direction()
    {
        int i;
        if (facingRight)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        return i;
    }
    bool canGrab()
    {
        bool cg;
        cg = Physics2D.OverlapCircle(transform.GetChild(0).transform.position, grabRadius, whatIsGrab);
        return cg;
    }
    void Grab()
    {
        Collider2D[] grab = Physics2D.OverlapCircleAll(transform.GetChild(0).transform.position, grabRadius, whatIsGrab);
        if (hasGrabbed)
        {           
            for(int i = 0; i<grab.Length; i++)
            {
                if(grabbedObj == null)
                {
                    grabbedObj = grab[0].gameObject;
                    grabbedObj.GetComponent<object_Traverse>().enabled = false;
                    
                }
                else
                {
                    Rigidbody2D grb;
                    grb = grabbedObj.GetComponent<Rigidbody2D>();
                    grb.isKinematic = true;
                    grabbedObj.transform.position = transform.GetChild(0).position;
                    grabbedObj.transform.parent = transform.GetChild(0);
                    grabbedObj.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
        else if(grabbedObj != null)
        {
            grabbedObj.transform.parent = null;
            grabbedObj.GetComponent<Collider2D>().enabled = true;
            grabbedObj.GetComponent<object_Traverse>().enabled = true;
            Rigidbody2D grb;
            grb = grabbedObj.GetComponent<Rigidbody2D>();
            grb.isKinematic = false;
            grb.AddForce(Vector2.right * direction()*(pushForce*(rb.velocity.x*direction())), ForceMode2D.Impulse);
            grb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
            grabbedObj = null;
        }

    }

    void traverse()
    {
        Vector2 lr = limits[0];
        Vector2 ud = limits[1];
        Vector2 pos = transform.position;

        if (pos.y < ud.x)
        {
            transform.position = new Vector2(pos.x, ud.y);
        }

        if (pos.y > ud.y)
        {
            transform.position = new Vector2(pos.x, ud.x);
        }

        if (pos.x < lr.x)
        {
            transform.position = new Vector2(lr.y, pos.y);
        }

        if (pos.x > lr.y)
        {
            transform.position = new Vector2(lr.x ,pos.y);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
