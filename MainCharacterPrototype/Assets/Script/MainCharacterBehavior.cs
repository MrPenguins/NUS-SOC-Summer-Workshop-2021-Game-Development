using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehavior : MonoBehaviour
{
    public float moveVelocity = 10f;
    public float jumpVelocity = 10f;
    public float doubleJumpVelocity;
    public bool ableDoubleJump;
    public bool ableChangeGravity;

    private Rigidbody2D myRidbody2D;
    private BoxCollider2D myFeet;
    private bool isOnGround;
    private bool doubleJump;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myRidbody2D = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnGround();
        Move();
    }

    void Move()
    {
        if (isAlive)
        {
            Run();
            Jump();
            ChangeGravity();
        }
    }

    void Run()
    {
        Vector2 runV = new Vector2(myRidbody2D.velocity.x, myRidbody2D.velocity.y);
        if (isOnGround)
            runV.x = 0;
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
        if (Input.GetKeyDown(KeyCode.Space))
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

    public void CheckOnGround()
    {
        isOnGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //Debug.Log(isOnGround);
    }
    
    public void OnCollisionEnter2D(Collision2D collisionObject)
    {
        if (collisionObject.collider.tag == "Trap")
        {
            Death();
        }
    }

    public void Death()
    {
        isAlive = false;
        Vector3 deathPosition = new Vector3(0,0,0);
        if (myRidbody2D.velocity.x >= 0)
        {
            deathPosition.z = 90;
        }
        else
        {
            deathPosition.z = -90;
        }
        transform.rotation = Quaternion.Euler(deathPosition);
    }

    public void ChangeGravity()
    {

        if (Input.GetKeyDown(KeyCode.W) && ableChangeGravity)
        {
            Vector3 newGravity = Physics2D.gravity;
            newGravity.y = -newGravity.y;
            Physics2D.gravity = newGravity;

            Vector3 newRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            if (newRotation.x == 0)
                newRotation.x = 180;
            else
                newRotation.x = 0;
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
    
}
