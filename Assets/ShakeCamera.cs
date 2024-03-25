using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShakeCamera : MonoBehaviour
{
    public float shakeIntensity = 0.1f; // Intensity of the shake
    public float shakeDuration = 0.4f; // Duration of the shake

    private Vector3 _originalPosition;

    public bool shakeTest;

    private void Awake()
    {
        _originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeTest)
        {
            ShakeCam();
        }
    }

    private void ShakeCam()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = _originalPosition + Random.insideUnitSphere * shakeIntensity;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0.4f;
            transform.localPosition = _originalPosition;
            shakeTest = false;
        }
    }
}