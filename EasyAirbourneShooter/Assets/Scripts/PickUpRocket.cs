using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRocket : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1f;

    Rigidbody2D rb;
    AudioPlayer audioPlayer;
    float rbXSize;
    float rbYSize;
    float sizeChangeSpeed = 0.01f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rbXSize = rb.GetComponent<Transform>().transform.localScale.x;
        rbYSize = rb.GetComponent<Transform>().transform.localScale.y;
    }
    void Update()
    {
        rbXSize -= sizeChangeSpeed;
        if (rbXSize <= 0 || rbXSize >= 1)
        {
            sizeChangeSpeed *= -1;
        }
        Move();
    }

    void Move()
    {
        rb.velocity = -transform.up * moveSpeed * Difficulty.multiplier;

        rb.transform.localScale = new Vector3(rbXSize, rbYSize, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioPlayer.playCollectClip();
            collision.GetComponent<Gun>().rocketsStacked++;
            Destroy(gameObject);
        }
        
    }
}
