using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenArrowBehavior : MonoBehaviour
{
    public bool mFollowMousePosition = true;
    public float mHeroSpeed = 20f;
    public float mHeroRotateSpeed = 90f / 2f; // 90-degrees in 2 seconds
    public float mHeroAccleration = 5f;
    private int mTotalEggCount = 0;
    private int touchedEnemy = 0;
    private int destoryEnemy = 0;
    public GameObject mCoolDown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Move this object to mouse position
        Vector3 p = transform.localPosition;
        p.z = 0f;

        if (Input.GetKeyDown(KeyCode.M))
            mFollowMousePosition = !mFollowMousePosition;

        if (mFollowMousePosition)
        {
            p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f;  // <-- this is VERY IMPORTANT!
            // Debug.Log("Screen Point:" + Input.mousePosition + "  World Point:" + p);
        }
        else
        {
            p += ((mHeroSpeed * Time.smoothDeltaTime) * transform.up);
            if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
                mHeroSpeed+=mHeroAccleration;

             if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
                mHeroSpeed-=mHeroAccleration;

            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(transform.forward,  mHeroRotateSpeed * Time.smoothDeltaTime);

            if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);


        }
        // Now spawn an egg when space bar is hit
        if (Input.GetKey(KeyCode.Space))
        {
            if (mCoolDown.GetComponent<CoolDownBar>().ReadyForNext())
            {         
                GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject); // Prefab MUST BE locaed in Resources/Prefab folder!
                e.transform.up=transform.up;
                e.transform.localPosition = transform.localPosition;
                // Debug.Log("Spawn Eggs:" + e.transform.localPosition);
                mTotalEggCount++;
                mCoolDown.GetComponent<CoolDownBar>().TriggerCoolDown();
            }            
        }

        transform.localPosition = p;

    }
    public void TouchedEnemy()
    {
        touchedEnemy++;
        destoryEnemy++;
    }

    public void DestoryEnemy()
    {
        destoryEnemy++;
    }
    
    public void OneLessEgg() { mTotalEggCount--;  }

    public string EggStatus() { return "Eggs on screen: " + mTotalEggCount; }

    public string HeroControlMode() 
    {
        if (mFollowMousePosition)
        {
            return "HERO: Drive(Mouse)";
        }
        else
        {
            return "HERO: Drive(Key)";
        }
    }

    public string ShowTouchedEnemy()
    {
        return "Touched Enemy: " + touchedEnemy;
    }

    public string EnemyStatus()
    {
        return "Enemy: Count(10) Destory(" + destoryEnemy+")";
    }

    //     private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("Arrow: OnTriggerEnter2D");
    //     collision.gameObject.transform.position = Vector3.zero;
    // }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     Debug.Log("Arrow: OnTriggerStay2D");
    //     collision.gameObject.transform.position = Vector3.zero;
    // }

}
