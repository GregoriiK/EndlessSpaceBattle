using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    Gun gun;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    [SerializeField] bool applyCameraShake = false;
    [SerializeField] public int health = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] public int scoreAmount = 50;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gun = GetComponent<Gun>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        if (gun.useAI)
        {
            health = Mathf.RoundToInt(health * Difficulty.multiplier);
            scoreAmount = Mathf.RoundToInt(scoreAmount * Difficulty.multiplier);
        }
        else
        {
            health = Mathf.RoundToInt(health / Difficulty.multiplier);
        }
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            audioPlayer.playHitClip();
        }
    }

    void Die()
    {
        audioPlayer.playDestroyClip();
        if (gun.useAI)
        {
            scoreKeeper.ScoreUpdate(scoreAmount);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
