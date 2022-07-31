using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    Vector2 basePosition;
    [SerializeField] Vector2 baseRange;
    float baseRangeDistance;

    public GameObject player;
    Vector2 playerPosition;
    float playerDistance;

    bool canTeleport = false;

    void Awake()
    {
        basePosition = this.transform.position;
        baseRangeDistance = Vector2.Distance(basePosition, baseRange);
    }

    // Update is called once per frame
    void Update()
    {
        Teleportable();
        if (Input.GetKeyDown(KeyCode.T) && canTeleport == true)
        {
            player.transform.position = basePosition;
        }
    }

    void Teleportable()
    {
        playerPosition = player.transform.position;
        playerDistance = Vector2.Distance(playerPosition, basePosition);

        if (baseRangeDistance >= playerDistance)
        {
            canTeleport = true;
        }
        else if (baseRangeDistance < playerDistance)
        {
            canTeleport = false;
        }
    }
}
