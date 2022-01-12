using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{    
    Vector3 startPos;
    float movementFactor;

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
