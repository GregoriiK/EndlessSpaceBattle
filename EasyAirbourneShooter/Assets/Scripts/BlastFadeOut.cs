using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFadeOut : MonoBehaviour
{
    float fadeOutTimer;
    void Start()
    {
        fadeOutTimer = 1f;
    }

    void Update()
    {
        fadeOutTimer -= Time.deltaTime*5;
        Color circleColor = GetComponent<SpriteRenderer>().color;
        circleColor.a = fadeOutTimer;
        GetComponent<SpriteRenderer>().color = circleColor;
        if (fadeOutTimer <= 0)
        {
            Destroy(gameObject);
        }

    }
}
