using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    #region VARIABLES
    public Action OnTimerEnd;
    public Action<float> OnTimerChange;
    public Action OnTimerTicking;
    private TextMeshProUGUI timerText;
    private Transform timerBar;
    private bool _timerIsRunning = false;
    private int totalTimeInSeconds;
    private int tickingSeconds;
    private float remainingTime;
    private bool timerTicking;

 

    #endregion

    #region UNITY METHODS
    private void Awake()
    {
       
    }
    void Update()
    {
        if (_timerIsRunning)
        {
            remainingTime -= Time.deltaTime;
            int time = Mathf.FloorToInt(remainingTime);
            if(timerBar != null)
            {
                timerBar.localScale = new Vector3(remainingTime / totalTimeInSeconds, 1, 1);
            }
            if (remainingTime > 0)
            {
                if (time != tickingSeconds)
                {
                    TimerTicking(time);
                }
            }
            else
            {
                EndTimer();
            }

        }
    }

    #endregion

    #region  PUBLIC METHODS
    public void Init(Action _action)
    {
        totalTimeInSeconds = GameSettings.sharedInstance.GameDuration;
        remainingTime = GameSettings.sharedInstance.GameDuration;
        tickingSeconds = GameSettings.sharedInstance.TickingSeconds;
        Reset();   
        OnTimerEnd = _action;
    }

    public void SetText(TextMeshProUGUI _timerText)
    {
        timerTicking = false;
        timerText = _timerText;
        DisplayTime(remainingTime);
    }
    public void Reset()
    {
        _timerIsRunning = false;
        OnTimerEnd = null;
        OnTimerChange = null;
        DisplayTime(totalTimeInSeconds);
    }
    public void StartTimer()
    {
        remainingTime = totalTimeInSeconds;
        _timerIsRunning = true;
        tickingSeconds = -1;
    }
    public void PauseTimer()
    {
        _timerIsRunning = false;
    }
     public void UnPauseTimer()
    {
        _timerIsRunning = true;
    }
    public float StopTimer()
    {
        _timerIsRunning = false;
        return remainingTime;
    }
    public string GetTotalTime()
    {
        return GetFormatSec(totalTimeInSeconds);
    }
    #endregion

    #region  PRIVATE METHODS
    private void TimerTicking(int _time)
    {
        tickingSeconds = _time;
        OnTimerChange?.Invoke(remainingTime);
        if (_time < 10 && _time >= 0)
        {
            if (timerTicking == false)
            {
                timerTicking = true;
            }
            tickingSeconds = _time;
            if (timerText != null)
                TimerTextBump(timerText.transform);
        }
        DisplayTime(remainingTime);
    }
    private void EndTimer()
    {
        //  AudioManager.sharedInstance.StopTimerEndingBgClip();
        Debug.Log("Time has run out!");
        _timerIsRunning = false;
        remainingTime = 0;
        if (timerText != null)
            timerText.text =  "0 sec";
        OnTimerChange?.Invoke(0);
        print(OnTimerEnd);
        OnTimerEnd?.Invoke();
    }
    private void DisplayTime(float timeToDisplay)
    {
        if (timerText == null) return;
        timerText.text = (((int)timeToDisplay)).ToString() + " sec";
        // timerText.text = GetFormatSec(timeToDisplay);
    }
    private Sequence TimerTextBump(Transform Obj)
    {
        AudioManager.sharedInstance.PlayTimerTickClip();
        OnTimerTicking?.Invoke();
        Sequence seq = DOTween.Sequence();
        seq.Append(Obj.DOScale(1.2f, 0.2f));
        seq.Append(Obj.DOScale(Vector3.one, 0.2f));
        return seq;
    }
    private string GetFormatSec(float _timeToDisplay)
    {
         int minutes = Mathf.FloorToInt(_timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(_timeToDisplay % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}
