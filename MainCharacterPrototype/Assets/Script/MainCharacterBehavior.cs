using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehavior : MonoBehaviour
{
    public float moveVelocity = 10f;
    public float jumpVelocity = 10f;
    public float doubleJumpVelocity;
    public bool ableDoubleJump;

    private Rigidbody2D myRidbody2D;
    private bool isOnGround;
    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        myRidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }

    void Run()
    {
        Vector2 runV = new Vector2(myRidbody2D.velocity.x, myRidbody2D.velocity.y);
        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                runV = new Vector2(-moveVelocity, myRidbody2D.velocity.y);
                transform.localScale = new Vector3(-System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            if (Input.GetKey(KeyCode.D))
            {
                runV = new Vector2(moveVelocity, myRidbody2D.velocity.y);
                transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

        }
        myRidbody2D.velocity = runV;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(isOnGround)
            {
                Vector2 jumpV = new Vector2(myRidbody2D.velocity.x, jumpVelocity);
                myRidbody2D.velocity = jumpV;
                if (ableDoubleJump)
                    doubleJump = true;

            }

            else if (doubleJump) 
            {
                Vector2 jumpV = new Vector2(myRidbody2D.velocity.x, doubleJumpVelocity);
                myRidbody2D.velocity = jumpV;
                doubleJump = false;
            }
            
        }
    }

    public void OnCollisionEnter2D(Collision2D collisionObject)//…Ë÷√≤Œ ˝
    {
        if (collisionObject.collider.tag == "Ground")
        {
            isOnGround = true;
        }
    }
    public void OnCollisionExit2D(Collision2D collisionObject)
    {
        if (collisionObject.collider.tag == "Ground")
        {
            isOnGround = false;
        }
    }

}
