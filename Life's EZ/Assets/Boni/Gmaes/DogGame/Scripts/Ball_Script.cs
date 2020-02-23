using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball_Script : MonoBehaviour
{
    public float yforcemultiplier, xforcemultiplier, xconstant, xvaria, 
        yvaria, minuscale, minusalpha, minx, miny;
    private float initx, inity, randx, randy;
    private bool flying = false, dying = false, dedsoon = false;
    private Rigidbody2D rb;
    private GameObject des;
    public Animator anim;
    GameManager gm;
    SpriteRenderer sp;
    AudioSource aSrc;
    public AudioClip[] borks;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        des = GameObject.FindGameObjectWithTag("DestroyTrig");
        gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
        sp.sortingOrder = 10;
        anim = GameObject.Find("body").GetComponent<Animator>();
        aSrc = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (des.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            dedsoon = true;
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
            anim.SetBool("open", true);
        }
        else
        {
            anim.SetBool("open", false);
        }
        if (dying)
        {
            Die();
        }
        if (flying)
        {
            transform.localScale = new Vector3(transform.localScale.x - minuscale, transform.localScale.y - minuscale,
                transform.localScale.z);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
        if (transform.localScale.x < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        initx = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        inity = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
    }
    private void OnMouseUp()
    {
        float diry = ((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - inity) * yvaria) * yforcemultiplier;
        float dirx = ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - initx) * xvaria) * xforcemultiplier;
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - inity > miny)
        {
            Vector2 direction = new Vector2(dirx, diry);
            rb.AddForce(direction);
            flying = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal" && 
            collision.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            gm.Goal();
            aSrc.PlayOneShot(borks[Random.Range(0, borks.Length)]);

        }
        if (collision.gameObject.tag == "DestroyTrig" && dedsoon == true)
        {
            dying = true;
        }
    }
    void Die()
    {
        Color color = GetComponent<SpriteRenderer>().material.color;
        color.a -= Time.deltaTime * minusalpha;
        GetComponent<SpriteRenderer>().material.color = color;
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -10;
    }
}