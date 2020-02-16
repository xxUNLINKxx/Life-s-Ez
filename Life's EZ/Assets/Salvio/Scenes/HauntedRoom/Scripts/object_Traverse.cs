using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_Traverse : MonoBehaviour
{

    [SerializeField] private Vector2[] limits;
    [SerializeField] private float maxVelocity;
    private Rigidbody2D rb;
    [SerializeField] public bool canPick;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        traverse();

        if (rb.velocity.y > maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
            rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        }
        if (rb.velocity.y < -maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocity);
        }
        if (rb.velocity.x > maxVelocity)
        {
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
            rb.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
        }
        if (rb.velocity.x < -maxVelocity)
        {
            rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
            rb.AddForce(-Vector2.right * 2, ForceMode2D.Impulse);
        }

        if(Mathf.Abs(rb.velocity.x)>= 1|| Mathf.Abs(rb.velocity.y) >= 1)
        {
            canPick = false;
        }
        else
        {
            canPick = true;
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
            transform.position = new Vector2(lr.x, pos.y);
        }

    }
}
