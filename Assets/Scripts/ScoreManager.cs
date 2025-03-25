using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int ActualScore { get; private set; }
    [SerializeField] private ScoreData scoreData;

    private UI_Manager _UIM;

    public void Initialize(UI_Manager uim)
    {
        _UIM = uim;

        ActualScore = 0;
        _UIM.UpdateText(ActualScore, scoreData.highScore);
    }

    public void UpdateScore(int scoreAmount)
    {
        ActualScore += scoreAmount;

        if (ActualScore > scoreData.highScore)
        {
            scoreData.highScore = ActualScore;
        }

        _UIM.UpdateText(ActualScore, scoreData.highScore);

        if (ActualScore >= GameManager.Instance.maxScore)
        {
            GameManager.Instance.GameOver();
        }   
    }
}