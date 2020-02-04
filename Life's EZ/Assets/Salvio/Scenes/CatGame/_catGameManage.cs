using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _catGameManage : MonoBehaviour
{
    private bool hasPressed = false,hasLost=false;
    private SceneTransition sceneTransition;
    catPlayerScript player;


    private void Start()
    {
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
        player = GameObject.Find("Player").GetComponent<catPlayerScript>();
        player.canMove = false;
    }

    private void Update()
    {
        if (!player.canMove&&Input.GetKeyDown(KeyCode.Space))
        {
            player.canMove = true;
        }
        if (hasLost)
        {
            if (Input.GetKeyDown(KeyCode.Space)&&!hasPressed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                hasPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && !hasPressed)
            {
                StartCoroutine(back2Menu());
                hasPressed = true;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            GameObject.Find("Player").GetComponent<catPlayerScript>().canMove = false;
            hasLost = true;
        }
    }

    IEnumerator back2Menu()
    {
        sceneTransition.StartCoroutine(sceneTransition.ExitScene(2f));
        yield return new WaitForSeconds(2f);
        sceneTransition.StartCoroutine(sceneTransition.EnterScene());
        SceneManager.LoadScene("Main");

    }
}
