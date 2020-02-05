using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _catGameManage : MonoBehaviour
{
    private bool hasPressed = false,hasLost=false;
    private SceneTransition sceneTransition;
    catPlayerScript player;
    [SerializeField] private GameObject Ball,startPanel, losePanel;
    [SerializeField] private Text finalScore;


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
            StartCoroutine(StartGame());
        }
        if(!player.canMove)
        {
            Ball.SetActive(false);
        }
        if (hasLost)
        {
            finalScore.text = "ur score: " + player.points;
            StartCoroutine(LoseGame());
            if (Input.GetKeyDown(KeyCode.Space)&& !hasPressed)
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

    IEnumerator StartGame()
    {
        player.canMove = true;
        LeanTween.scale(startPanel, new Vector2(0,0), 0.5f).setEaseInBounce().setEaseLinear();
        yield return new WaitForSeconds(1.8f);       
        Ball.SetActive(true);
    }

    IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.scale(losePanel, new Vector2(1, 1), 0.5f).setEaseInBounce().setEaseLinear();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            player.canMove = false;
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
