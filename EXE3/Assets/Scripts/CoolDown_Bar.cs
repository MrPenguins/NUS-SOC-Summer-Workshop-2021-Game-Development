using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
