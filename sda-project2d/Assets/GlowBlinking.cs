using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlowBlinking : MonoBehaviour
{
    private TMP_Text text;

    [SerializeField] private float minTime, maxTime;

    private float offsetTime;
    private float time;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time > offsetTime)
        {
            text.enabled = !text.enabled;
            time = 0;
            offsetTime = Random.Range(minTime, maxTime);
        }
    }

}
