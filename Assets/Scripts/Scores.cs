using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BestScoreData
{
    public int score = 0;
}

public class Scores : MonoBehaviour
{
    public Text socreText;

    
    private BestScoreData bestScores_ = new BestScoreData();
    private int currentScores_;

    private string bestScoreKey_ = "bsdat";


    void Start()
    {
        currentScores_ = 0;
        
        UpdateScoreText();
        
    }

    private void OnEnable()
    {
        GameEvents.AddScores += AddScores;
    }

    private void OnDisable()
    {
        GameEvents.AddScores -= AddScores;
    }

    private void AddScores(int socres)
    {
        currentScores_ += socres;
        if(currentScores_ > bestScores_.score)
            bestScores_.score = currentScores_;
        
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        socreText.text = currentScores_.ToString();
    }

}
