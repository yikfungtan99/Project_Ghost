using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Flicker : MonoBehaviour
{
    public float frequency = 0;
    public float minLightIntensity;
    public float maxLightIntensity;
    private float frequencyTimer = 0;

    private Light2D light;

    private void Start()
    {
        light = transform.GetComponent<Light2D>();
        frequencyTimer = Random.Range(0,frequency);
    }


    void Update()
    {
        if(frequencyTimer <= 0)
        {
            light.intensity = Random.Range(minLightIntensity, maxLightIntensity);
            frequencyTimer = Random.Range(0, frequency);
        }
        else
        {
            frequencyTimer -= Time.deltaTime;
        }
        
    }

}
