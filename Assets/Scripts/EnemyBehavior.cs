using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyBehavior : MonoBehaviour
{
    float enemyRangeDistance;
    float playerDistance;
    [HideInInspector] public bool canDash;
    Vector2 enemyPosition;
    Vector2 playerPosition;
    public GameObject enemyRange;
    [SerializeField] GameObject player;

    [HideInInspector] public bool mustPatrol;
    bool mustTurn;

    public float walkSpeed;
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    void Start()
    {
        mustPatrol = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        enemyPosition = this.transform.position;
        enemyRangeDistance = Vector2.Distance(enemyPosition, enemyRange.transform.position);
        Condition();
    }

    void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }    
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }

        rb.velocity = new Vector2 (walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
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
