using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BrickOutBoi : MonoBehaviour
{
    public GameObject[] GameObjects;
    public Vector3 Velocity;
    public float PaddleSpeed;
    public float limit;
    public GameObject brickHolder;
    public GameObject gameHolder;
    public GameObject menuHolder;
    public GameObject textbox;
    public GameObject[] startTextBox;
    bool hasStarted=false,haspressed = false;
    private SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        gameHolder.SetActive(true);
        menuHolder.SetActive(false);
        startTextBox[0].SetActive(true);
        startTextBox[1].SetActive(false);
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasStarted = true;
                startTextBox[1].SetActive(true);
                startTextBox[0].SetActive(false);
                GameObjects[1].GetComponent<Rigidbody2D>().velocity = Velocity;
            }
            if (Input.GetKeyDown(KeyCode.Escape)&&!haspressed)
            {
                haspressed = true;
                Leave();
            }
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Replay();
            }
        }
        if (GameObjects[1].transform.position.y<-10)
        {
            OnDeath();
        }
     if (Input.GetKey(KeyCode.LeftArrow))
        {
            if ((GameObjects[0].transform.position + new Vector3(-PaddleSpeed, 0f, 0f)).x <-limit)
            {
                GameObjects[0].transform.position = new Vector3(-limit, GameObjects[0].transform.position.y, GameObjects[0].transform.position.z);
            }
            else
            {
                GameObjects[0].transform.position = GameObjects[0].transform.position + new Vector3(-PaddleSpeed, 0f, 0f);
            }
        }
            else if (Input.GetKey(KeyCode.RightArrow))
        {
            if ((GameObjects[0].transform.position + new Vector3(PaddleSpeed, 0f, 0f)).x > limit)
            {
                GameObjects[0].transform.position = new Vector3(limit, GameObjects[0].transform.position.y, GameObjects[0].transform.position.z);
            } else
            {
                GameObjects[0].transform.position = GameObjects[0].transform.position + new Vector3(PaddleSpeed, 0f, 0f);
            }
        } 
     else
      {
            GameObjects[0].transform.position = GameObjects[0].transform.position + new Vector3(0f, 0f, 0f);
        }
        CheckIfWon();
    }
    void OnDeath()
    {
        gameHolder.SetActive(false);
        startTextBox[0].SetActive(false);
        startTextBox[1].SetActive(false);
        menuHolder.SetActive(true);
        textbox.GetComponent<Text>().text ="You lost!\nYour Score: " + DetermineScore();
    }
    public void Leave()
    {
        StartCoroutine(leave());
    }
    public void Replay()
    {
        SceneManager.LoadScene("BrickOutBoi");
    }
    int DetermineScore()
    {
        int score = (50 - brickHolder.transform.childCount);
        if (score == 50)
        {
            return 5000;
        }
        return score*100;
    }
    public void CheckIfWon()
    {
        if (brickHolder.transform.childCount == 0)
        {               
            textbox.GetComponent<Text>().text = "You Won!\nYo       ur Score: " + DetermineScore();
            gameHolder.SetActive(false);
            menuHolder.SetActive(true);
        }
    }
    IEnumerator leave()
    {
        sceneTransition.StartCoroutine(sceneTransition.ExitScene(2f));
        yield return new WaitForSeconds(2f);
        sceneTransition.StartCoroutine(sceneTransition.EnterScene());       
        SceneManager.LoadScene("Main");
    }
}
