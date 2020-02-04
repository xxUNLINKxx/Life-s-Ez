using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class catPlayerScript : MonoBehaviour
{
    private float moveInput;
    [SerializeField] private float speed, bounceSpeed, points, attackRadius;
    [SerializeField] public bool canMove;
    [SerializeField] private Text pointsText;
    [SerializeField] private Animator catArm, platform;

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

        if (Distance() < attackRadius)
        {
            catArm.SetTrigger("attack");
        }
    }

    float Distance()
    {
        Transform ball = GameObject.FindGameObjectWithTag("Ball").transform;

        float d = Vector2.Distance(ball.position, transform.position);
        return d;
    }
    void Bounce(Rigidbody2D ball, int rand)
    {
        ball.velocity = Vector2.zero;
        ball.AddForce(Vector2.up * bounceSpeed, ForceMode2D.Impulse);
        if (rand > 0)
        {
            ball.AddForce(Vector2.left * 0.2f, ForceMode2D.Impulse);
            ball.AddTorque(1f, ForceMode2D.Impulse);
        }
        else
        {
            ball.AddForce(-Vector2.left * 0.2f, ForceMode2D.Impulse);
            ball.AddTorque(-1f, ForceMode2D.Impulse);
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
