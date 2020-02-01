using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    private float moveInput;
    private int index,maxIndex;
    private bool move, movingLeft;
    [SerializeField] private Transform[] movePos;
    [SerializeField] private Transform sprite;
    [SerializeField] private float speedTime, startST;
    private void Start()
    {
        maxIndex = movePos.Length-1;
        movingLeft = false;
    }
    void Update()
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
}


