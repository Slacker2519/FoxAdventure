using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    Vector2 basePosition;
    [SerializeField] GameObject baseRange;
    float baseRangeDistance;

    public GameObject player;
    Vector2 playerPosition;
    float playerDistance;

    bool canTeleport = false;

    // Update is called once per frame
    void Update()
    {
        basePosition = this.transform.position;
        baseRangeDistance = Vector2.Distance(basePosition, baseRange.transform.position);

        TeleportHandler();
        TeleportPlayer();

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("player in range to teleport:  " + canTeleport);
            Debug.Log("the range of teleport base:  " + baseRangeDistance);
            Debug.Log("distance between player and teleport base  " + playerDistance);
        }
    }

    void TeleportPlayer()
    {
        if (Input.GetMouseButtonDown(0) && canTeleport == true)
        {
            player.transform.position = basePosition;
        }
    }

    void TeleportHandler()
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
