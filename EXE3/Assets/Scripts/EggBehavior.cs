using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    static private GreenArrowBehavior sGreenArrow = null;
    static public void SetGreenArrow(GreenArrowBehavior g) { sGreenArrow = g; }

    private const float kEggSpeed = 40f;
    private bool killMySelf;
    // Start is called before the first frame update
    void Start()
    {
        killMySelf=false;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);

        // 1. Find the main camera and get the CameraSupport component
        CameraSupport s = Camera.main.GetComponent<CameraSupport>();  // Try to access the CameraSupport component on the MainCamera
        if (s != null)   // if main camera does not have the script, this will be null
        {
            // intersect my bond with the bounds of the world
            Bounds myBound = GetComponent<Renderer>().bounds;  // this is the bound on the SpriteRenderer
            CameraSupport.WorldBoundStatus status = s.CollideWorldBound(myBound);
            
            // If result is not "inside", then, move the hero to a random position
            if (status != CameraSupport.WorldBoundStatus.Inside)
            {
                killMySelf=true;
            }
        }

        if (killMySelf)
        {
            DestroyEgg();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var name = collision.name;
        Debug.Log("collide with " + name);
        if(name!="Hero" && name!="Egg(Clone)")
        {
        DestroyEgg();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var name = collision.name;
        Debug.Log("collide with " + name);
        if(name!="Hero" && name!="Egg(Clone)")
        {
        DestroyEgg();
        }
    }

    private void DestroyEgg()
    {
        Destroy(transform.gameObject);  // kills self
        sGreenArrow.OneLessEgg();
    }
}
