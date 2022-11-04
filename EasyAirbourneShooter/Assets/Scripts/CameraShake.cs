using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] public float shakeMagnitude = 0.5f;

    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float shakeTime = 0f;
        while (shakeTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
