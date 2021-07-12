using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane: MonoBehaviour
{
    private int damage=0;

    static private GreenArrowBehavior sGreenArrow = null;
    static public void SetGreenArrow(GreenArrowBehavior g) { sGreenArrow = g; }

    // Start is called before the first frame update
    void Start()
    {
        randomPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateColor()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;
        c.a*=0.8f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with " + collision.name);
        if (collision.name=="Hero")
        {
            
            randomPosition();
            sGreenArrow.TouchedEnemy();
        }
        else
        {
            damage++;
            if (damage==4){
                randomPosition();
                RefreshColor();
                damage=0;
                sGreenArrow.DestoryEnemy();
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
        if (collision.name=="Hero")
        {
            randomPosition();
            sGreenArrow.TouchedEnemy();
        }
        else
        {
            damage++;
            if (damage==4){
                randomPosition();
                RefreshColor();
                damage=0;
                sGreenArrow.DestoryEnemy();
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
        p.x = s.GetWorldBound().min.x + s.GetWorldBound().size.x*0.05f + Random.value * s.GetWorldBound().size.x*0.85f;
        p.y = s.GetWorldBound().min.y + s.GetWorldBound().size.y*0.05f + Random.value * s.GetWorldBound().size.y*0.85f;
        p.z=0f;
        transform.localPosition=p;
    }
}
