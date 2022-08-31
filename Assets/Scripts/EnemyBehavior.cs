using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float enemyRangeDistance;
    float playerDistance;
    [HideInInspector] public bool canDash;
    Vector2 enemyPosition;
    Vector2 playerPosition;
    public GameObject enemyRange;
    [SerializeField] GameObject player;

    // Update is called once per frame
    void Update()
    {
        enemyPosition = this.transform.position;
        enemyRangeDistance = Vector2.Distance(enemyPosition, enemyRange.transform.position);
        Condition();
    }

    void Condition()
    {
        canDash = false;
        playerPosition = player.transform.position;
        playerDistance = Vector2.Distance(playerPosition, enemyPosition);
        if (enemyRangeDistance >= playerDistance)
        {
            canDash = true;
        }
        else if(enemyRangeDistance < playerDistance)
        {
            canDash = false;
        }
    }
}
