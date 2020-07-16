using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    //This is the index of the next scene
    [SerializeField] int nextLevel;

    void OnTriggerEnter2D (Collider2D other)
    {
        //If the player reaches the end, start the next level
        if (other.gameObject.tag.Equals ("Player")) {
            SceneManager.LoadSceneAsync(nextLevel);
        }
    }
}
