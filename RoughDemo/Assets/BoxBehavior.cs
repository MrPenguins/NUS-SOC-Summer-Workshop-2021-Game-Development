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

        if (col.tag == "mCamera") 
        {
            boxFixedJoint2D.enabled = true;
        }
            
        
        else if (col.tag == "Player")
        {
            boxFixedJoint2D.enabled = false;
            /*
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
            */
        }
        
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject col = collision.collider.gameObject;

        if (col.tag == "mCamera")
            boxFixedJoint2D.enabled = true;
        /*
        GameObject[] objects = new GameObject[10];
        int i = 0;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            //Debug.Log(contact.collider .gameObject);
            objects[i] = contact.collider.gameObject;
            i++;
        }
        for (int j = 0; j < i; j++)
        {
            if (objects[j].tag == "mCamera")
            {
                boxFixedJoint2D.enabled = true;
                return;
            }

        }
        for (int j = 0; j < i; j++)
        {
            if (objects[j].tag == "Player")
            {
                boxFixedJoint2D.enabled = false;
                return;
            }

        }
        */
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        boxRigidbody2D.velocity = new Vector3(0, 0, 0);
        boxFixedJoint2D.enabled = true;
    }
}
