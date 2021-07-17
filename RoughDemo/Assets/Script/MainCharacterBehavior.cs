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
    public bool isOnCamera;
    public bool isOnGround;
    public bool isAlive = true;

    public Animator anim;

    private bool Idle;
    private float InputX;
    private float running;
    private float FloatOnGround;


    private Rigidbody2D myRidbody2D;
    private BoxCollider2D myFeet;
    private float Jumping;
    private float Falling;
    private bool doubleJump;
    // Start is called before the first frame update
    void Start()
    {
        myRidbody2D = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        isAlive = true;
        anim.SetBool("isAlive", true);
        anim = GetComponent<Animator>();
        anim.SetFloat("InputX", 0);
        anim.SetFloat("Jumping", 0);
        anim.SetFloat("Falling", 0);
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
        if (isOnGround || isOnCamera)
            runV.x = 0;
        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                runV = new Vector2(-moveVelocity, myRidbody2D.velocity.y);
                transform.localScale = new Vector3(-System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                anim.SetFloat("running", 1);
                anim.SetFloat("InputX", -1);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                runV = new Vector2(moveVelocity, myRidbody2D.velocity.y);
                transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                anim.SetFloat("running", 1);
                anim.SetFloat("InputX", 1);
            }
            else
            {
                anim.SetFloat("running", 0);
                anim.SetFloat("InputX", 0);
            }
        }
        myRidbody2D.velocity = runV;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isOnGround || isOnCamera)
            {
                anim.SetFloat("Jumping", 1);
                anim.SetFloat("Falling", 0);
                Vector2 jumpV = new Vector2(myRidbody2D.velocity.x, jumpVelocity);
                myRidbody2D.velocity = jumpV;
                if (ableDoubleJump)
                    doubleJump = true;

            }

            else if (doubleJump)
            {
                anim.SetFloat("Jumping", 1);
                anim.SetFloat("Falling", 0);
                Vector2 jumpV = new Vector2(myRidbody2D.velocity.x, doubleJumpVelocity);
                myRidbody2D.velocity = jumpV;
                doubleJump = false;
            }
            else
            {
                anim.SetFloat("Falling", 1);
                anim.SetFloat("Jumping", 0);
            }
        }
        else if (!isOnGround && !isOnCamera)
        {
            anim.SetFloat("Falling", 1);
            anim.SetFloat("Jumping", 0);
        }
        else if (isOnGround && (running == 0f) || isOnCamera && (running == 0f))
        {
            anim.SetBool("Idle", true);
            anim.SetFloat("Falling", 0);
            anim.SetFloat("Jumping", 0);
        }
    }

    public void CheckOnGround()
    {
        isOnGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        isOnCamera = myFeet.IsTouchingLayers(LayerMask.GetMask("mist"));
        if (isOnGround || isOnCamera)
        {
            anim.SetFloat("FloatOnGround", 1);
        }
        else
        {
            anim.SetFloat("FloatOnGround", 0);
        }
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
        anim.SetBool("isAlive", false);
        isAlive = false;
        Vector3 deathPosition = new Vector3(0, 0, 0);
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
