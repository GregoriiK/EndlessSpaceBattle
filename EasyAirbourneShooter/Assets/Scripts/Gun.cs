using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] public float projectileVelocity = 20f;
    [SerializeField] float projectileLifespan = 3f;
    [SerializeField] float baseFiringRate = 0.3f;

    [Header("AI Related")]
    [SerializeField] public bool useAI;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0.1f;

    [HideInInspector] public bool isFiring;
    [HideInInspector] public bool shootingSecondary;

    AudioPlayer audioPlayer;
    Coroutine fireCoroutine;

    float alternateFire = 0.3f;
    public int rocketsStacked = 0;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        rocketsStacked = 1;
        if (useAI)
        {
            isFiring = true;
            alternateFire = 0f;
            projectileVelocity *= Difficulty.multiplier;
        }
    }

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContiuously());
        }
        else if(!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator FireContiuously()
    {
        while (true)
        {   
            Vector3 firePositionX = new Vector3(alternateFire, -0.7f, 0); //gun location on the ship
            firePositionX += transform.position;
            GameObject instance = Instantiate(projectilePrefab, firePositionX, Quaternion.identity);
            audioPlayer.playShootingClip();

            Rigidbody2D rb;
            rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = transform.up * projectileVelocity;
            }

            Destroy(instance, projectileLifespan);
            alternateFire *= -1;
            float timeToNextProjectile = Random.Range(baseFiringRate - fireRateVariance
                                                    , baseFiringRate + fireRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFireRate, float.MaxValue);
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    public void ShootRocket()
    {
        if (rocketsStacked >= 1)
        {
            GameObject instance = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            rocketsStacked--;
        }
    }

    public int GetCurrentRocketsCout()
    {
        return rocketsStacked;
    }
}
