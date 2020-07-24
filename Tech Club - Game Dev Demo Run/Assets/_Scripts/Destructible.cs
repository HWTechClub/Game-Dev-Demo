using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    //These are its drops
    [SerializeField] GameObject[] drops;

    //When it gets struck, call this.
    public void GetDestroyed (Vector2 playerPos)
    {
        //Drops all of its contents as new instantiated objects
        foreach (GameObject drop in drops)
        {
            GameObject obj = Instantiate(drop, transform.position, drop.transform.rotation);
            
        }

        //Destroys this object
        Destroy(this.gameObject);
    }
}
