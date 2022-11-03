using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    static ScoreKeeper instance;

    int currentScore = 0;

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

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ScoreUpdate(int scoreAmount)
    {
        currentScore += scoreAmount;
        Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
