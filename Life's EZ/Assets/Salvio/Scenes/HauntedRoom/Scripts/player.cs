using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player : MonoBehaviour
{

    [SerializeField] public float moveInput, speed, jumpForce, pushForce, groundCheckRadius, grabRadius,maxVelocity;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask whatIsGround,whatIsGrab;
    [SerializeField] private Vector2[] limits;
    private bool facingRight = true, hasGrabbed = false;
    private GameObject grabbedObj;
    private Animator anim;
    public int area;
    [SerializeField] private GameObject pC, canvas;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        traverse();
        traverseClone();
        ctrlVelocty();
        moveInput = Input.GetAxis("Horizontal");
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
        if(direction() == 1)
        {
            Transform canva = canvas.transform;
            Vector2 newScale = new Vector2(0.1f, 0.1f);
            canvas.transform.localScale = newScale;
        }
        else
        {
            Transform canva = canvas.transform;
            Vector2 newScale = new Vector2(-0.1f, 0.1f);
            canvas.transform.localScale = newScale;
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

    public int direction()
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
                if(grabbedObj == null && grab[0].GetComponent<object_Traverse>().canPick)
                {
                    grabbedObj = grab[0].gameObject;
                    grabbedObj.GetComponent<object_Traverse>().enabled = false;
                    grabbedObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    anim.SetBool("holding", true);
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
            anim.SetBool("holding", false);
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
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (pos.x < lr.x)
        {
            transform.position = new Vector2(pC.transform.position.x, pos.y);
        }

        if (pos.x > lr.y)
        {
            transform.position = new Vector2(pC.transform.position.x, pos.y);
        }

    }
    void traverseClone()
    {
        Vector2 lr = limits[2];
        Vector2 pos = transform.position;

        if (pos.x < lr.x)
        {
            pC.GetComponent<SpriteRenderer>().enabled = true;
            area = 1;
        }

        if (pos.x > lr.y)
        {
            pC.GetComponent<SpriteRenderer>().enabled = true;
            area = -1;
        }

        if(pos.x > lr.x && pos.x < lr.y)
        {
            pC.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    void ctrlVelocty()
    {
        if (rb.velocity.y > maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
        }
        else if (rb.velocity.y < -maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocity);
        }
        else if (rb.velocity.x > maxVelocity)
        {
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxVelocity)
        {
            rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
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
