using System;
using Jambav.Utilities;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    #region  VARIABLES
    public Action <CompetitorCard> OnCompetitorSelected;
    public Action <ProductCard> OnProductSelected;
    public Action OnPlayButtonPressed;
    public Action OnTouchDetected;
    bool touchInteracted = false;
    #endregion

    #region UNITY_METHODS
    void Start()
    {
        Input.multiTouchEnabled = false;
    }
    void Update()
    {
        if(!touchInteracted)
            DetectTouch();
        touchInteracted = false;

        if(Input.GetKeyUp(KeyCode.R))
        {
            GameManager.sharedInstance.gamePlayController.InitGame();
        }

        // #if UNITY_EDITOR
        // if (GameManager.sharedInstance.currentGameState == GameState.Home)
        //     ForHome();
        // else if (GameManager.sharedInstance.currentGameState == GameState.GamePlay || GameManager.sharedInstance.currentGameState == GameState.Tutorial)
        //     ForGamePlay();
        // else if (GameManager.sharedInstance.currentGameState == GameState.EndGame)
        //     ForGameEnd();
        
    }

    #endregion

    public void CompetitorSelected(CompetitorCard _card)
    {
        touchInteracted = true;
        OnCompetitorSelected?.Invoke(_card);
    }
     public void ProductSelected(ProductCard _card)
    {
        touchInteracted = true;
        OnProductSelected?.Invoke(_card);
    }
    public void PlayButtonPressed()
    {
        touchInteracted = true;
        OnPlayButtonPressed?.Invoke();
    }
    private void DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                 OnTouchDetected?.Invoke();
            }
        }
        if(Input.GetMouseButtonUp(0)) // Detects mouse click or touch
        {
            OnTouchDetected?.Invoke();
        }
       
    }
    private void ForHome()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
             
        }
       
    }
    private void ForGamePlay()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            
        }
    }
    private void ForGameEnd()
    {
        
    }
}
