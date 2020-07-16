using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealConsumable : MonoBehaviour
{
    [SerializeField] int healAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag.Equals ("Player"))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();

            pc.TakeHeal(healAmount);

            Destroy(this.gameObject);
        }
    }
}
