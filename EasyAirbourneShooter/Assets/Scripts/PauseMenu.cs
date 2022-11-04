using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject confirmationMenu;
    bool returnToMainMenu;

    LevelManager levelManager;
    CameraShake cameraShake;
    
    float tempShakeMagnitude;
    float tempSfxVolume;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        tempSfxVolume = AudioPlayer.sfxVolumeControl;
        tempShakeMagnitude = cameraShake.shakeMagnitude;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        cameraShake.shakeMagnitude = 0f;
        AudioPlayer.sfxVolumeControl = 0f;
        pauseMenu.SetActive(true);
        pauseButton.interactable = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.interactable = true;
        Time.timeScale = 1;
        cameraShake.shakeMagnitude = tempShakeMagnitude;
        AudioPlayer.sfxVolumeControl = tempSfxVolume;
    }

    public void ReturnToMainMenu()
    {
        pauseMenu.SetActive(false);
        confirmationMenu.SetActive(true);
        returnToMainMenu = true;
    }

    public void QuitGamePrompt()
    {
        pauseMenu.SetActive(false);
        confirmationMenu.SetActive(true);
        returnToMainMenu = false;
    }

    public void Confirm()
    {
        if (returnToMainMenu)
        {
            Time.timeScale = 1;
            AudioPlayer.sfxVolumeControl = tempSfxVolume;
            levelManager.LoadMainMenu();
        }
        else
        {
            AudioPlayer.sfxVolumeControl = tempSfxVolume;
            levelManager.QuitGame();
        }
    }

    public void Decline()
    {
        pauseMenu.SetActive(true);
        confirmationMenu.SetActive(false);
    }
}
