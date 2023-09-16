using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchAnimation : MonoBehaviour
{
    [SerializeField] private float breathingSpeed = 1.0f;
    [SerializeField] private float radiusMultiplier = 0.1f;
    [SerializeField] private Light2D light2D;
    
    private float originalRadius;
    private float timeOffset;

    private void Start()
    {
        originalRadius = light2D.pointLightOuterRadius;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update()
    {
        float time = Time.time + timeOffset;
        float radiusOffset = Mathf.Sin(time * breathingSpeed) * radiusMultiplier;

        light2D.pointLightOuterRadius = originalRadius + radiusOffset;
    }
}
