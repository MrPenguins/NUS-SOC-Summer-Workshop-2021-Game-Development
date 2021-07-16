using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    Rigidbody2D boxRigidbody2D;
    GameObject mainCharacter;
    FixedJoint2D boxFixedJoint2D;
    // Start is called before the first frame update
    void Start()
    {
        boxRigidbody2D = GetComponent<Rigidbody2D>();
        boxFixedJoint2D = GetComponent<FixedJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.collider.gameObject;
        if (col.tag == "Player") 
        {
            mainCharacter = col;
            if (Input.GetKeyDown(KeyCode.S))
            {
                boxFixedJoint2D.enabled = true;
                boxFixedJoint2D.connectedBody = mainCharacter.GetComponent<Rigidbody2D>();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                boxFixedJoint2D.enabled = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        boxRigidbody2D.velocity = new Vector3(0, 0, 0);
    }
}
