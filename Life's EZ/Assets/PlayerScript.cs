using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    private float moveInput;
    private int index, maxIndex;
    private bool down, hasMoved;
    [SerializeField] private Transform[] movePos;
    [SerializeField] private Transform sprite;
    private void Start()
    {
        maxIndex = movePos.Length;
        hasMoved = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.D)))
        {
            down = true;
            hasMoved = !hasMoved;
        }
        else
        {
            down = false;
        }
        if (down)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (index < maxIndex)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = maxIndex;
                }
            }
            NextPos(index);
        }
        Waddle();
    }

    void NextPos(int index)
    {
        transform.position = movePos[index].position;
    }

    void Waddle()
    {
        if (hasMoved)
        {
            sprite.Rotate(0, 0, -10);
        }
        else
        {
            sprite.Rotate(0, 0, 10);
        }
    }
}


