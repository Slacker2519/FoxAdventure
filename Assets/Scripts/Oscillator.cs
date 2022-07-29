using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector2 startingPosition;
    [SerializeField] Vector2 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        OscillatingHandler();
    }

    void OscillatingHandler()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;   //continually growing over time

        const float tau = Mathf.PI * 2;  //constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    //recalculated to go from 0 to 1 so it's cleaner

        Vector2 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
