using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mCameraBehaviourPt2 : MonoBehaviour
{
    private float moveSpeed = 20f;

    private GameObject[] children;
    private GameObject hero = null;


    private float[] boundary;//left:x  right:x  up:y  down:y
    private float LRemptySpace = 2f;
    private float UDemptySpace = 3f;
    private bool[] moveAble;//right left down up
   
    void Start()
    {

        children = new GameObject[5];
        children[0] = GameObject.Find("leftMist");
        children[1] = GameObject.Find("rightMist");
        children[2] = GameObject.Find("upMist");
        children[3] = GameObject.Find("downMist");
        children[4] = GameObject.Find("sight");

        hero = GameObject.Find("MainCharacter");

        moveAble = new bool[4];
        for (int i = 0; i < 4; i++)
            moveAble[i] = true;

        boundary = new float[4];
        getBound();
    }

    // Update is called once per frame
    void Update()
    {
        getBound();
        checkMove();
        move();

    }


    private void move()
    {
        Vector3 p = transform.localPosition;
        if (Input.GetKey(KeyCode.LeftArrow) && moveAble[0])
        {
            p -= transform.right * (moveSpeed * Time.smoothDeltaTime);
            transform.position += transform.up * (moveSpeed * Time.smoothDeltaTime);
            transform.localPosition = p;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && moveAble[1])
        {
            p += transform.right * (moveSpeed * Time.smoothDeltaTime);
            transform.position += transform.up * (moveSpeed * Time.smoothDeltaTime);
            transform.localPosition = p;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && moveAble[2])
        {
            p += transform.up * (moveSpeed * Time.smoothDeltaTime);
            transform.position += transform.up * (moveSpeed * Time.smoothDeltaTime);
            transform.localPosition = p;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && moveAble[3])
        {
            p -= transform.up * (moveSpeed * Time.smoothDeltaTime);
            transform.position += transform.up * (moveSpeed * Time.smoothDeltaTime);
            transform.localPosition = p;
        }
        else
        {
            //empty
        }


    }

    private void checkMove()
    {

        // Vector3 parentP = hero.transform.parent.position;
        //Vector3 p = parentP + hero.transform.localPosition;
        Vector3 p =  hero.transform.localPosition;
         bool isOnCamera = hero.GetComponent<MainCharacterBehavior>().isOnCamera;
         bool isOnGround = hero.GetComponent<MainCharacterBehavior>().isOnGround;
         bool isAlive= hero.GetComponent<MainCharacterBehavior>().isAlive;

        //这里要找到hero父节点的position
        if (p.x <= boundary[0])//left
            moveAble[1] = false;
        else
            moveAble[1] = true;

        if (p.x >= boundary[1])//right
            moveAble[0] = false;
        else
            moveAble[0] = true;

        if (p.y >= boundary[2])//up
            moveAble[3] = false;
        else
            moveAble[3] = true;

        if (p.y <= boundary[3])//down
        {//想添加一个人物跳跃的时候不能向上移动摄像头的功能
            //人物不在地上的时候，这里也要设为false

            moveAble[2] = false;
        }
        else
            moveAble[2] = true;

        if (isOnCamera)
        {
            moveAble[2] = false;
            moveAble[3] = false;
        }
        if (!isOnGround)
        {
            moveAble[2] = false;
            moveAble[3] = false;
        }
        if (!isAlive)
        {
            moveAble[0] = false;
            moveAble[1] = false;
            moveAble[2] = false;
            moveAble[3] = false;
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(boundary[0]);
            Debug.Log(boundary[1]);
            Debug.Log(boundary[2]);
            Debug.Log(boundary[3]);
            Debug.Log(p.x);
            Debug.Log(p.y);
            Debug.Log(moveAble[0]);
            Debug.Log(moveAble[1]);
            Debug.Log(moveAble[2]);
            Debug.Log(moveAble[3]);
        }

    }

    private void getBound()
    {
        Vector3 basep = transform.position;//父物体坐标

        GameObject child = children[0];//left
        Vector3 p = basep + child.transform.localPosition;//这样才得到世界坐标
        Vector3 s = child.transform.localScale;
        float x = p.x + 0.5f * s.x + LRemptySpace;
        boundary[0] = x;

        child = children[1];//right
        p = child.transform.localPosition + basep;
        s = child.transform.localScale;
        x = p.x - 0.5f * s.x - LRemptySpace;
        boundary[1] = x;

        child = children[2];//up
        p = child.transform.localPosition + basep;
        s = child.transform.localScale;
        x = p.y - 0.5f * s.y - UDemptySpace;
        boundary[2] = x;

        child = children[3];//down
        p = child.transform.localPosition + basep;
        s = child.transform.localScale;
        x = p.y + 0.5f * s.y + UDemptySpace;
        boundary[3] = x;

    }
}
