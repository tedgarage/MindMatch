using System;
using UnityEngine;
using UnityEngine.AI;

public class ScoreHandler : MonoBehaviour
{
    #region VARIABLES
    public Action<int, int> OnScoreUpdate;
    public Action<int> OnStreakUpdate;
    private int totalScore;
    private int finishedProductsCount;
    private int streakValue;
    private int maxStreak;
    private bool init;

    private float streakTotalLifeTime;
    private float streakLifeTime;
    

    #endregion
    #region  Unity Methods

    void Update()
    {

       

    }
    #endregion
    #region PUBLIC_METHODS
    public void InitScoreHandler()
    {
        totalScore = 0;
        init = true;
        streakValue=0;
        maxStreak = 0;
        finishedProductsCount =0;
        streakTotalLifeTime = Constants.STREAK_DURATION_SEC;

        // tutorialRunning = true;
    }
    public void GameStart()
    {
    
    }
    public (int _score,int _changedScore,bool _itsHighScore) BoxPlaced(float _remainingPercentage, bool _correctDirection)
    {
        if (!init) { return (0,0,false); }

        finishedProductsCount++;
        if (_remainingPercentage != 0)
            AddStreak(_correctDirection);

        int changedScore = (int)(GetScoreForProductFinish(_remainingPercentage, _correctDirection) * GetStreakMultiplayer());
        totalScore += changedScore;
        OnScoreUpdate?.Invoke(changedScore, totalScore);
        bool itsHighScore=false;
         int currentBestScore = GetBestScore();
        if (currentBestScore < totalScore)
        {
            itsHighScore = true;
        }

        return (totalScore, changedScore,itsHighScore);
    }
    public void AddStreak(bool _correctDirection)
    {
        if(GameManager.sharedInstance.currentGameState == GameState.Tutorial)
        {
            return;
        }
        if (_correctDirection)
        {
            streakValue++;
            OnStreakUpdate?.Invoke(streakValue);
            streakLifeTime = streakTotalLifeTime;
            if (maxStreak < streakValue)
                maxStreak = streakValue;
        }
        else
            ResetStreak();
    }
    public void ResetStreak()
    {
        streakValue = 0;
        OnStreakUpdate?.Invoke(streakValue);
    }
    public int GetStreak()
    {
        return streakValue;
    }
    public int GetMaxStreak()
    {
        return maxStreak;
    }
    public (int _score,bool _itsHighScore)  GetGameScoreAfterGameOver()
    {
        bool itsHighScore = false;
        int currentBestScore = GetBestScore();
        if (currentBestScore < totalScore)
        {
            itsHighScore = true;
            PlayerPrefs.SetInt(Constants.BEST_SCORE_KEY, totalScore);
            PlayerPrefs.Save();
        }
        return (totalScore,itsHighScore);
    }
    public int GetFinishedProductsCount()
    {
        return finishedProductsCount;
    }
    public int GetBestScore()
    {
        if (PlayerPrefs.HasKey(Constants.BEST_SCORE_KEY))
        {
            return PlayerPrefs.GetInt(Constants.BEST_SCORE_KEY);
        }
        else
        {
            return 0;
        }
    }

    #endregion

    #region PRIVATE_METHODS
    private int GetScoreForProductFinish(float _percentage, bool _direction)
    {
        print("Direction "+_direction);
        print("Percentage "+_percentage);
        _percentage = _percentage*100;
        int score = 0;
        if(GameManager.sharedInstance.currentGameState == GameState.Tutorial)
        {
            score = Constants.CORRECT_ANSWER_ON_TIME;
        }
        else if (_percentage == 0)
        {
            score = Constants.CORRECT_ANSWER_AFTER_DISMISS;
        }
        else
        {
            float scoreMultiplier = 0;
            if (_percentage >= Constants.THREE_STAR_REMAINING_TIME_PERCENTAGE)
            {
                scoreMultiplier = Constants.THREE_STAR_MULTIPLIER;
            }
            else if (_percentage >= Constants.TWO_STAR_REMAINING_TIME_PERCENTAGE)
            {
                scoreMultiplier = Constants.TWO_STAR_MULTIPLIER;
            }
            else
            {
                scoreMultiplier = Constants.ONE_STAR_MULTIPLIER;
            }
            print("score multiplier " + scoreMultiplier);
            
            score =(int) (Constants.CORRECT_ANSWER_ON_TIME  * scoreMultiplier);
        }
        
        if(!_direction)
        {
            score = score/2;
        }

        return score * Constants.SCORE_MULTIPLIER;
    }
    private float GetStreakMultiplayer()
    {
        return 1 + (streakValue * .1f);

    }

    #endregion
}
