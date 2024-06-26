using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    
    public GameObject gameOverPopup;
    public GameObject loosePopup;
    public GameObject newBestScorePopup;

    public GameObject highScore;


    void Start()
    {
        gameOverPopup.SetActive(false);
           
    }


    private void OnEnable()
    {
        GameEvents.GameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.GameOver -= OnGameOver;
    }

    private void OnGameOver(bool newBestScore)
    {
        gameOverPopup.SetActive(true);
        highScore.SetActive(true);
        loosePopup.SetActive(false);
        newBestScorePopup.SetActive(true);
        
    }
}
