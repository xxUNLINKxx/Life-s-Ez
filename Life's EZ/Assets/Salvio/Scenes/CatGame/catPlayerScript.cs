﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class catPlayerScript : MonoBehaviour
{
    private float moveInput;
    [SerializeField] private float speed, bounceSpeed, points;
    [SerializeField] public bool canMove;
    [SerializeField] private Text pointsText;

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (canMove)
        {
            rb.velocity = new Vector2(moveInput * speed, 0);
        }
        pointsText.text = points.ToString();
    }
    void Bounce(Rigidbody2D ball, int rand)
    {
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