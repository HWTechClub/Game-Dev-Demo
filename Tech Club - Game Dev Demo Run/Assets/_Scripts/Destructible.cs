using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    //These are its drops
    [SerializeField] GameObject[] drops;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDestroyed (Vector2 playerPos)
    {
        foreach (GameObject drop in drops)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }

        Destroy(this.gameObject);
    }
}
