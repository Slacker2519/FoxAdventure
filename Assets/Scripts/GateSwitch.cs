using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    [SerializeField] GameObject crankup;
    [SerializeField] GameObject crankdown;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject switchRange;
    [SerializeField] GameObject player;
    bool canSwitch;
    float playerDistance;
    float activateSwitchRange;
    float timer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        activateSwitchRange = Vector2.Distance(crankup.transform.position, switchRange.transform.position);
        crankdown.SetActive(false);
        crankup.SetActive(true);
    }

    void Update()
    {
        SwitchCondition();

        if (Input.GetKeyDown(KeyCode.R) && canSwitch == true)
        {
            OpenGate();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                CloseGate();
            }
        }
    }

    private void SwitchCondition()
    {
        playerDistance = Vector2.Distance(crankup.transform.position, player.transform.position);
        if (playerDistance <= activateSwitchRange)
        {
            canSwitch = true;
        }
        else
        {
            canSwitch = false;
        }
    }

    void CloseGate()
    {
        gate.SetActive(true);
        crankup.SetActive(true);
        crankdown.SetActive(false);
    }

    void OpenGate()
    {
        gate.SetActive(false);
        crankdown.SetActive(true);
        crankup.SetActive(false);
    }
}
