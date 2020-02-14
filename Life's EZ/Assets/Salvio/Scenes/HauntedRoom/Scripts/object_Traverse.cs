using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_Traverse : MonoBehaviour
{

    [SerializeField] private Vector2[] limits;

    private void Update()
    {
        traverse();
    }
    void traverse()
    {
        Vector2 lr = limits[0];
        Vector2 ud = limits[1];
        Vector2 pos = transform.position;

        if (pos.y < ud.x)
        {
            transform.position = new Vector2(pos.x, ud.y);
        }

        if (pos.y > ud.y)
        {
            transform.position = new Vector2(pos.x, ud.x);
        }

        if (pos.x < lr.x)
        {
            transform.position = new Vector2(lr.y, pos.y);
        }

        if (pos.x > lr.y)
        {
            transform.position = new Vector2(lr.x, pos.y);
        }

    }
}
