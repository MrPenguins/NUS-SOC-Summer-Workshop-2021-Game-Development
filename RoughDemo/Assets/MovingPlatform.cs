using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movingVelocity;
    public int movingFrame;

    private bool movingRight;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
        if (movingRight)
        {
            newPosition.x += movingVelocity;
            i++;
        }
        else
        {
            newPosition.x -= movingVelocity;
            i++;
        }

        if (i == movingFrame)
        {
            i = 0;
            movingRight = !movingRight;
        }

        transform.position = newPosition;
    }
}
