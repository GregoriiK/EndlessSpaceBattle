using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float timeToActivation;
    [SerializeField] float acceleration;
    [SerializeField] ParticleSystem detonationParticles;
    [SerializeField] GameObject detonationCircle;
    CircleCollider2D rocketCollider;
    Rigidbody2D rb;
    ContactFilter2D contactFilter2D;
    List<Collider2D> listOfColliders;

    float speed;
    float timer;
    static public bool rocketDeployed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rocketCollider = GetComponent<CircleCollider2D>();
        rocketDeployed = true;
        rocketCollider.enabled = false;
        speed = 0f;
        timer = 0f;
        listOfColliders = new List<Collider2D>();
        contactFilter2D.NoFilter();
    }

    void Update()
    {
        RocketMovement();
        timer += Time.deltaTime;
        if (timer > timeToActivation)
        {
            RocketDetonation();
        }

        
    }

    void ActivateCollider()
    {
        rocketCollider.enabled = true;
        rocketCollider.OverlapCollider(contactFilter2D, listOfColliders);

        foreach(Collider2D collider in listOfColliders)
        {
            if (collider != null && collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Health>().Die();
            }
        }
    }

    void RocketMovement()
    {
        if (speed < 10f)
        {
            speed += acceleration;
        }
        rb.velocity = transform.up * speed;
    }

    void RocketDetonation()
    {
        ActivateCollider();
        PlayDetonationEffect();
        Camera.main.GetComponent<CameraShake>().Play();
        Destroy(rb.gameObject);
        rocketDeployed = false;
    }

    void PlayDetonationEffect()
    {
        if (detonationParticles != null)
        {
            ParticleSystem instance = Instantiate(detonationParticles, transform.position, transform.rotation);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }

        GameObject circleInstance = Instantiate(detonationCircle, transform.position, transform.rotation);
    }

}
