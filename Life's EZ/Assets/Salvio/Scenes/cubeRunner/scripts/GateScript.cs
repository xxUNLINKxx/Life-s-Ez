using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour
{
    [SerializeField] private Transform[] pos;
    [SerializeField] private GameObject[] Levels;
    [SerializeField] private int posIndex, maxIndex;
    [SerializeField] private GameObject Player, Panel, Exit;
    [SerializeField] private Timer GetTimer;
    private bool hasEntered, hasWon;
    private SceneTransition sceneTransition;

    void setActiveLevel()
    {
        for (int i = 0; i < maxIndex; i++)
        {
            if (i == posIndex)
            {
                Levels[i].SetActive(true);
            }
            else
            {
                Levels[i].SetActive(false);
            }
        }
    }
    private void Start()
    {
        maxIndex = pos.Length;
        hasEntered = false;
        setActiveLevel();
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, pos[posIndex].position, 2f*Time.fixedDeltaTime);
    }
    private void Update()
    {
        Win();
        ExitGame();
        if (!hasWon)
        {
            Lose();    
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!hasEntered) 
        {
            if (!hasWon)
            {
                StartGame();
                hasEntered = true;
                posIndex += 1;
                StartCoroutine(LoadLevel(0.3f));
            }
            else
            {
                ResetGame();
                //for now
            }
        }
    }

    IEnumerator LoadLevel(float delay)
    {
        Time.timeScale = 0;
        GetTimer.startTimer = false;
        yield return new WaitForSecondsRealtime(delay);
        setActiveLevel();
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
        GetTimer.startTimer = true;
        hasEntered = false;
    }
    
    bool lose = false;
    void Lose()
    {

        if (Player.transform.position.y <= -7)
        {
            if (posIndex <= 0)
            {
                Exit.SetActive(true);
            }
            else
            {
                Panel.SetActive(true);
                lose = true;
            }
            StopGame();

        }
        else
        {
            Panel.SetActive(false);
            Exit.SetActive(false);
            lose = false;
        }

        if (lose &&Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        Player.transform.position = Vector2.zero;
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        posIndex = 0;
        hasWon = false;
        GetTimer.ResetTimer();
        GetTimer.gameObject.SetActive(false);
        setActiveLevel();
    }

    void Win()
    {
        if(posIndex >= maxIndex-1)
        {
            hasWon = true;
            StopGame();
        }
        else
        {
            hasWon = false;
        }
    }

    void StartGame()
    {
        GetTimer.gameObject.SetActive(true);
        GetTimer.startTimer = true;
    }

    void StopGame()
    {
        GetTimer.startTimer = false;
    }

    void ExitGame()
    {
        if (Exit.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
            }
            //escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //exitCode
                StartCoroutine(back2Menu());
            }
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
