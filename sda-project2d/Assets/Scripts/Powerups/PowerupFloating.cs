using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFloating : MonoBehaviour
{
    [SerializeField] private float frequency, amplitude;
    private Vector2 tempPos, posOffset;

    private void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
