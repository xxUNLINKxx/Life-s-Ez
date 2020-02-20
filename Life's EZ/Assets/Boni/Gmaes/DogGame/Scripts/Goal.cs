using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float limxp, limxn, speed;
    private float randx;
    // Start is called before the first frame update
    void Start()
    {
        randx = 0;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(randx, transform.position.y, transform.position.z), speed);
    }
    public void ChangePos()
    {
        randx = Random.Range(limxn, limxp);
    }
}
