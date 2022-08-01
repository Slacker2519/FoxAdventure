using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderHandler : MonoBehaviour
{
    [SerializeField] Animator deadVfx;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var objTag = collision.gameObject.tag;

        if (objTag == "Goal")
        {
            LoadNextLevel();
        }
        else if (objTag == "Enemy")
        {
            Dead();
            Invoke("ReloadLevel", 1f);
            Debug.Log(objTag);
        }
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

    void Dead()
    {
        deadVfx.SetBool("IsDead", true);
        GetComponent<Movement>().enabled = false;
    }
}
