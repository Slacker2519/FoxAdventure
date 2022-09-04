using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderHandler : MonoBehaviour
{
    [SerializeField] Animator deadVfx;

    [HideInInspector] public Collider2D other;
    public Movement movement;
    public EnemyBehavior enemyBehavior;

    void OnTriggerEnter2D(Collider2D collision)
    {
        other = collision;
        var objTag = collision.gameObject.tag;

        if (objTag.Equals("Goal"))
        {
            LoadNextLevel();
        }
        if (objTag.Equals("Spikes"))
        {
            Dead();
            Invoke("ReloadLevel", 1f);
        }
        if (objTag.Equals("Enemy") && movement.isDashing == true)
        {
            Destroy(collision.gameObject);
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
