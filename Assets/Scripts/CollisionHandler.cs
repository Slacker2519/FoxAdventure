using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //var objTag = collision.gameObject.tag;

        if (collision.gameObject.tag == "Goal")
        {
            //LoadNextLevel();
            Debug.Log(collision.gameObject.tag);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            //ReloadLevel();
            Debug.Log(collision.gameObject.tag);
        }
    }

    void Update()
    {
        
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
    }
}
