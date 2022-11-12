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
    [SerializeField] GameObject rocketPickUp;
    [SerializeField] GameObject healthPickUp;

    int maxHealth;

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
            maxHealth = Mathf.RoundToInt(health * Difficulty.multiplier);
            health = maxHealth;
            scoreAmount = Mathf.RoundToInt(scoreAmount * Difficulty.multiplier);
        }
        else
        {
            maxHealth = Mathf.RoundToInt(health / Difficulty.multiplier);
            health = maxHealth;
        }
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public void PowerUpHealth()
    {
        int healthBoost = Mathf.RoundToInt(40 / Difficulty.multiplier);
        if (health+healthBoost<maxHealth)
        {
            health += healthBoost;
        }
        else
        {
            health = maxHealth;
        }
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

    public void Die()
    {
        audioPlayer.playDestroyClip();
        if (gun.useAI)
        {
            scoreKeeper.ScoreUpdate(scoreAmount);
            PowerUpLotery();
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

    public void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public void PowerUpLotery()
    {
        int chanceForPowerUp = Random.Range(0, 100);
        if (chanceForPowerUp >= 95)
        {
            GameObject instance = Instantiate(rocketPickUp, transform.position, Quaternion.identity);
        }
        else if (chanceForPowerUp <= 5)
        {
            GameObject instance = Instantiate(healthPickUp, transform.position, Quaternion.identity);
        }
    }
}
