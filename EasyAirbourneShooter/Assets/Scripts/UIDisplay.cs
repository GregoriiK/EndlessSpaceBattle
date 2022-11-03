using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider healthBar;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        health = FindObjectOfType<Health>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        healthBar.maxValue = health.GetCurrentHealth();
    }
    private void Update()
    {
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("000000000");
        healthBar.value = health.GetCurrentHealth();
    }
}
