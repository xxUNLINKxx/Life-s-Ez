using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class catPlayerScript : MonoBehaviour
{
    private float moveInput;
    [SerializeField] public float speed, bounceSpeed, points, attackRadius;
    [SerializeField] public bool canMove;
    [SerializeField] private Text pointsText;
    [SerializeField] private Animator catArm, platform;
    Transform ballTransform;

    private void Start()
    {
        ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Mathf.Abs(rb.velocity.x)> 0.1)
        {
            platform.SetBool("walk", true);
        }
        else
        {
            platform.SetBool("walk", false);
        }
        if (canMove)
        {
            rb.velocity = new Vector2(moveInput * speed, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        pointsText.text = points.ToString();

        if (Distance() < attackRadius && canMove)
        {
            catArm.SetTrigger("attack");
        }
    }

    float Distance()
    {
        float d = Vector2.Distance(ballTransform.position, transform.position);
        return d;
    }
    float multiplier()
    {
        float m = 0;
        if (points == 10)
        {
            m = 0.2f;
        }
        else if(points==20)
        {
            m = 0.3f;
        }
        else if (points == 30)
        {
            m = 0.4f;
        }
        else if (points == 40)
        {
            m = 0.2f;
        }
        else if (points == 50)
        {
            m = 0.1f;
        }
        else
        {
            m = 0;
        }

        return m;
    }
    void Bounce(Rigidbody2D ball, int rand)
    {
        ball.velocity = Vector2.zero;
        ball.AddForce(Vector2.up * (bounceSpeed+=multiplier()), ForceMode2D.Impulse);
        if (rand > 0)
        {
            ball.AddForce(Vector2.left * 0.35f, ForceMode2D.Impulse);
            ball.AddTorque(0.1f, ForceMode2D.Impulse);
        }
        else
        {
            ball.AddForce(-Vector2.left * 0.35f, ForceMode2D.Impulse);
            ball.AddTorque(-0.1f, ForceMode2D.Impulse);
        }
        if (canMove)
        {
            points += 1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            int rand = Random.Range(0, 2);
            Bounce(collision.rigidbody, rand);
        }
    }
}
