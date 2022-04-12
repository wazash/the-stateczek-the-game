using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusWaveMovement : MonoBehaviour
{
    [SerializeField] private float multiplier;

    private float time;

    public void Update()
    {
        time += Time.deltaTime;

        SinusWaveMove();
    }

    private void SinusWaveMove()
    {
        transform.position += 5f * Mathf.Sin(time * 2f + 1f) * Time.deltaTime * Vector3.up;
    }
}
