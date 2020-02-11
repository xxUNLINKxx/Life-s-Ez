using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    private float moveInput;
    [SerializeField]private int index,maxIndex;
    private bool move, movingLeft, hasPressed, canMove = true;
    [SerializeField] private Transform[] movePos;
    [SerializeField] private Transform sprite;
    [SerializeField] private float speedTime, startST;
    GameSelectionDataManager gData;
    SceneTransition sceneTransition;
    public GameObject menu;
    private void Start()
    {
        maxIndex = movePos.Length-1;
        movingLeft = false;
        gData = GameObject.Find("Data Manager").GetComponent<GameSelectionDataManager>();
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
        menu.SetActive(false);
    }
    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))
            {
                move = true;
            }
            else
            {
                move = false;
                startST = 0;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (move)
            {
                if (startST <= 0)
                {
                    //move right
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        if (index < maxIndex)
                        {
                            index++;
                        }
                        else
                        {
                            if(index == maxIndex)
                            {
                                index = maxIndex;
                            }
                            else
                            {
                                index = 0;
                            }

                        }
                    }
                    //move left
                    if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        if (index > 0)
                        {
                            index--;
                        }
                        else
                        {
                            if (index == 0)
                            {
                                index = 0;
                            }
                            else
                            {
                                index = maxIndex;
                            }
                        }
                    }
                    NextPos(index);
                    startST = speedTime;
                }
                else
                {
                    startST -= Time.deltaTime;
                }
            }
        }
        ActivateCom();
    }

    void NextPos(int index)
    {
        transform.position = movePos[index].position;
        movingLeft = !movingLeft;
        WalkAnim();
    }

    void WalkAnim()
    {
        if (movingLeft)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -10);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 10);
        }
    }

    void ActivateCom()
    {

        if(index <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E)&&canMove)
            {
                menu.SetActive(true);
                canMove = false;
                Debug.Log("this works");
            }
            else if (Input.GetKeyDown(KeyCode.E) && !canMove)
            {
                menu.SetActive(false);
                canMove = true;
            }
        }
        else
        {
            menu.SetActive(false);
        }

        if(index >= 6)
        {
            if (gData.canBePressed() && Input.GetKeyDown(KeyCode.E))
            {                          
                StartCoroutine(NextDay());
            }
        }
    }

    IEnumerator NextDay()
    {
        Debug.Log("This works");
        sceneTransition.StartCoroutine(sceneTransition.ExitScene(2f));
        yield return new WaitForSeconds(2f);
        sceneTransition.StartCoroutine(sceneTransition.EnterScene());
        gData.GenerateGames();
    }
}


