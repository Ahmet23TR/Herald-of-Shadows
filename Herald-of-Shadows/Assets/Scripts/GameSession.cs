using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] int PlayerScore = 0;
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = PlayerScore.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(PlayerLives > 1)
        {
            TakeLife();
        }
        else 
        {
            ResetGameSession();
        }
    }
    public void AddToScore(int pointsToAdd)
    {
        PlayerScore += pointsToAdd;
        ScoreText.text = PlayerScore.ToString();
    }
    public void AddToLife(int heartsToAdd)
    {
        PlayerLives += heartsToAdd;
        LivesText.text = PlayerLives.ToString();
    }

    void TakeLife()
    {
        PlayerLives--;
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
        LivesText.text = PlayerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePercist>().ResetPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
