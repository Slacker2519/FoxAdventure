using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
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
            StartCoroutine(Dead());
            StartCoroutine(ReloadLevel());
            Debug.Log("fucking work man!!!");
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

    IEnumerator ReloadLevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
        yield return new WaitForSeconds(5f);
    }

    IEnumerator Dead()
    {
        deadVfx.SetBool("IsDead", true);
        yield return new WaitForSeconds(2f);
    }
}
