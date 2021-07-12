using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{
    public int initial_x=70;
    public int initial_y=-70;
    private int damage=0;
    private bool visible=true;
    // Start is called before the first frame update
    void Start()
    {
        randomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            changeVisibility();
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with " + collision.name);
        if (collision.name=="Egg(Clone)")
        {
            
            damage++;
            if (damage==4){
                randomPosition();
                RefreshColor();
                damage=0;
            }
            else
            {
            UpdateColor();
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collide with " + collision.name);
        if (collision.name=="Egg(Clone)")
        {
            
            damage++;
            if (damage==4){
                randomPosition();
                RefreshColor();
                damage=0;
            }
            else
            {
            UpdateColor();
            }
        }
    }

    private void randomPosition(){
        Vector3 p;
        CameraSupport s = Camera.main.GetComponent<CameraSupport>();
        p.x = initial_x + Random.value * 30 - 15;
        p.y = initial_y + Random.value * 30 - 15;
        p.z=0f;
        transform.localPosition=p;
    }

        private void UpdateColor()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;
        c.a-=0.25f;
        s.color=c;
        // if (c.a <= 0.0f)
        // {
        //     Sprite t = Resources.Load<Sprite>("Textures/Egg");   // File name with respect to "Resources/" folder
        //     s.sprite = t;
        //     s.color = Color.white;
        // }
    }

    private void RefreshColor()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;
        c.a=1f;
        s.color=c;
    }

    private void changeVisibility()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;
        if (visible)
        {
            visible=false;
            c.a=0f;
        }
        else
        {
            visible=true;
            c.a=1f;
        }
        s.color=c;
    }
}
