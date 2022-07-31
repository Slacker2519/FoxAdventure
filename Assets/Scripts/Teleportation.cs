using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    Vector2 basePosition;   //position of teleport base
    [SerializeField] Vector2 baseRange;     //position of furthest range teleport base can reaches
    float baseRangeDistance;    //distance between teleport base position and furthest teleport range position

    public GameObject player;
    Vector2 playerPosition;     //position of player
    float playerDistance;   //distance between player and teleport base

    bool canTeleport = false;   //condition to teleport

    void Awake()
    {
        basePosition = this.transform.position;
        baseRangeDistance = Vector2.Distance(basePosition, baseRange);
    }

    // Update is called once per frame
    void Update()
    {
        TeleportHandler();
        TeleportPlayer();

        if (Input.GetKeyDown(KeyCode.P))    //measure some numbers for design game
        {
            Debug.Log("player in range to teleport:  " + canTeleport);
            Debug.Log("the range of teleport base:  " + baseRangeDistance);
            Debug.Log("distance between player and teleport base  " + playerDistance);
        }
    }

    void TeleportPlayer()   //press key to teleport player to teleport base
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
