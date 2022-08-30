using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderHandler : MonoBehaviour
{
    [SerializeField] Animator deadVfx;

    public Movement movement;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var objTag = collision.gameObject.tag;

        if (objTag.Equals("Goal"))
        {
            LoadNextLevel();
        }
        else if (objTag.Equals("Enemy") && movement.isDashing == false)
        {
            Dead();
            Invoke("ReloadLevel", 1f);
        }
        else if (objTag.Equals("Enemy") && movement.isDashing == true)
        {
            Destroy(collision.GetComponentInParent<EnemyBehavior>().gameObject);
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
