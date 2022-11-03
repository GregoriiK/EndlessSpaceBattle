using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 0.5f;

    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 0.5f;

    [Header("Destroy")]
    [SerializeField] AudioClip destroyClip;
    [SerializeField] [Range(0f, 1f)] float destroyVolume = 1f;

    static AudioPlayer instance;

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void playShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void playHitClip()
    {
        PlayClip(hitClip, hitVolume);
    }

    public void playDestroyClip()
    {
        PlayClip(destroyClip, destroyVolume);
    }

    void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
        }
    }
}
