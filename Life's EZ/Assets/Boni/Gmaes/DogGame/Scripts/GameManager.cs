using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public int startphase;
    public GameObject ball;
    private GameObject player;
    private Vector3 initpos;
    public Goal goal;
    public TextMeshProUGUI scoretext;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initpos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        scoretext.text = "" + score;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Instantiate(ball, initpos, transform.rotation);
        }
    }
    public void Goal()
    {
        score++;
        if (score > startphase)
        {
            goal.ChangePos();
        }
    }
}
