using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClone : MonoBehaviour
{
    private GameObject Player;
    private Animator anim;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = playerPos();
        transform.localScale = Player.transform.localScale;
        if (Mathf.Abs(Player.GetComponent<player>().moveInput) > 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    public Vector2 playerPos()
    {
        int direction = Player.GetComponent<player>().area;
        float x = Player.transform.position.x;
        float y = Player.transform.position.y;
        Vector2 playerPos = new Vector2(x + (10.2f*direction), y);
        return playerPos;
    }
}
