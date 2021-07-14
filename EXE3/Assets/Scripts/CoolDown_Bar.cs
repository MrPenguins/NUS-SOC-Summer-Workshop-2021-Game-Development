using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* USAGE!在greenup里面另外还需要操作如下

public GameObject mCoolDown;
//在unity里面，创建一个image
//把canvas的image拖到greenUp的mCoolDown来赋值

if (Input.GetKey(KeyCode.Space))
        {
            if (mCoolDown.GetComponent<CoolDownBar>().Ready())
            {
                //Here spawn an egg
                mCoolDown.GetComponent<CoolDownBar>().TriggerCoolDown();
            }
        }


*/

public class CoolDownBar : MonoBehaviour
{
    public float interval = 1f;
    private float lastAt = 0f;
    private bool active = false;
    private float width = 0f;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform r = GetComponent<RectTransform>();
        width = r.sizeDelta.x;

        lastAt = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            UpdateCoolDownBar();
    }

    private void UpdateCoolDownBar()
    {
        float sec = SecondsTillNext();
        float per = sec / interval;
        if(sec < 0)
        {
            active = false;
            per = 1f;
        }

        Vector2 s = GetComponent<RectTransform>().sizeDelta;
        s.x = per * width;
        GetComponent<RectTransform>().sizeDelta = s;
    }

    public void setInterval(float s) { interval = s; }

    private float SecondsTillNext()
    {
        float sec = -1;
        if (active)
        {
            sec = interval - (Time.time - lastAt);
            //当此项为零，长度为0
        }
        return sec;
    }

    public bool TriggerCoolDown()
    {
        bool flag = !active;
        if (flag)
        {
            active = true;
            lastAt = Time.time;//重新设置为最长长度
            UpdateCoolDownBar();
        }
        return flag;
    }

    public bool Ready()
    {
        return !active;
    }
}
