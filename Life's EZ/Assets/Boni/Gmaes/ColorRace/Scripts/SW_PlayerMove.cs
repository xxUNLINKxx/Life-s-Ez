using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SW_PlayerMove : MonoBehaviour
{
    public int health;
    public float maxspeed, acceleration, speed, deceleration;
    public ParticleSystem ps;
    private float startangdrag, startOrthoSize, startspeed;
    private Rigidbody2D rb;
    private Vector2 screenpos;
    public GameObject trail;
    public GameManagerScript GMS;
    void Start()
    {
        startspeed = speed;
        rb = GetComponent<Rigidbody2D>();
        startangdrag = GetComponent<Rigidbody2D>().angularDrag;
        startOrthoSize = Camera.main.orthographicSize;
    }
    void FixedUpdate()
    {
        CameraFunctions();
        MovementVelocity();
    }
    void CameraFunctions()
    {
        GameObject CamLook = GameObject.FindGameObjectWithTag("LookAt");
        if (speed > startOrthoSize)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 16f, Time.deltaTime * 0.5f);
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
                startOrthoSize, Time.deltaTime * 0.5f);
        }
        Camera.main.transform.position -= (Camera.main.transform.position -
            new Vector3(CamLook.transform.position.x, CamLook.transform.position.y, -10f)) * Time.deltaTime * 10f;
    }
    void MovementVelocity()
    {
        if (speed > maxspeed)
        {
            speed = maxspeed;
        }
        if (speed < -maxspeed)
        {
            speed = -maxspeed;
        }
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f)
        {
            GetComponent<Rigidbody2D>().angularDrag = startangdrag;
            GetComponent<Rigidbody2D>().AddTorque(-Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            GetComponent<Rigidbody2D>().angularDrag = 10f;
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            speed = startspeed - 5;
        }
        else {
            speed = startspeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Next")
        {
            GMS.index++;
            GMS.ActivateLevel();
        }
        if (collision.tag == "Enemy")
        {
            PlayerDestroy();
        }
        if (collision.tag == "Fish")
        {
            GMS.Victory();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerDestroy();
    }
    void PlayerDestroy()
    {
        Instantiate(ps, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
