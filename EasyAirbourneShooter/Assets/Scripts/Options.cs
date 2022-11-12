using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    float currentMultiplier;
    float currentMainVolume;
    float currentMusicVolume;
    float currentSfxVolume;
    bool currentMouseControlState;

    [SerializeField] GameObject pointer;

    [Header("Volume Control")]
    [SerializeField] Slider mainVolume;
    [SerializeField] Slider musicVolume;
    [SerializeField] public Slider sfxVolume;

    [Header("Other Controls")]
    [SerializeField] Toggle mouseControl;
    [SerializeField] GameObject instructions;

    RectTransform rectTransform;
    void Start()
    {
        currentMultiplier = Difficulty.multiplier;
        instructions.SetActive(false);

        currentMusicVolume = FindObjectOfType<AudioSource>().volume;
        musicVolume.value = currentMusicVolume;
        mainVolume.value = AudioPlayer.mainVolume;
        sfxVolume.value = AudioPlayer.sfxVolumeControl;
        currentMainVolume = mainVolume.value;
        currentSfxVolume = sfxVolume.value;

        currentMouseControlState = PlayerMovement.followMouse;
        mouseControl.isOn = PlayerMovement.followMouse;

        rectTransform = pointer.GetComponent<RectTransform>();
    }

    private void Update()
    {
        SetPointerPosition();
        SetAudioVolume();
    }

    public void SetMouseControl()
    {
        PlayerMovement.followMouse = mouseControl.isOn;
    }

    void SetAudioVolume()
    {
        AudioPlayer.sfxVolumeControl = sfxVolume.value;
        AudioPlayer.mainVolume = mainVolume.value;
        FindObjectOfType<AudioSource>().volume = musicVolume.value * mainVolume.value;
    }

    void SetPointerPosition()
    {
        if (Difficulty.multiplier == 1)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        else if (Difficulty.multiplier < 1)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector3(0, -50, 0);
        }
        else
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0f);
            rectTransform.anchorMax = new Vector2(0.5f, 0f);
            rectTransform.anchoredPosition = new Vector3(0, 50, 0);
        }
    }

    public void OnCancel()
    {
        Difficulty.multiplier = currentMultiplier;
        AudioPlayer.sfxVolumeControl = currentSfxVolume;
        sfxVolume.value = currentSfxVolume;
        mainVolume.value = currentMainVolume;
        musicVolume.value = currentMusicVolume;
        FindObjectOfType<AudioSource>().volume = currentMusicVolume;
        mouseControl.isOn = currentMouseControlState;
        PlayerMovement.followMouse = currentMouseControlState;
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
    }
}
