using System;
using System.Collections;
using DG.Tweening;
using Jambav.Utilities;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
       

#region VARIABLES
    public GamePlayController gamePlayController;
    public UIController uiController;
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public GameState currentGameState { get; private set; }
    #endregion

    #region UNITY_METHODS

    void Start()
    {
        DOTween.SetTweensCapacity(50, 500);
        if (GameSettings.sharedInstance.initialState == GameState.Home)
        {
            ChangeState(GameSettings.sharedInstance.initialState,false);
        }
        else
        {
            DOTween.Sequence().AppendInterval(.5f).AppendCallback(() =>
            {
                ChangeState(GameSettings.sharedInstance.initialState,true);
            });
        }
       
    }
      void Update()
    {
        // if(Input.GetKeyUp(KeyCode.Space))
        // {
        //     serialPortManager.SendMessageToSerialPort("0");
        // }
    }
    #endregion

    #region PUBLIC_METHODS
    public void ChangeState(GameState _newState,bool _byForced = false)
    {
        print("The Game State Changed");
        OnBeforeStateChanged?.Invoke(_newState);

        currentGameState = _newState;
        switch (_newState)
        {
            case GameState.None:
                print("The Game State is None");
                break;
            case GameState.Home:
                StartCoroutine(EnableHome(_byForced));
                break;
            case GameState.Tutorial:
                EnableTutorial(_byForced);
                break;
            case GameState.GamePlay:
                EnableGamePlay(_byForced);
                break;
            case GameState.EndGame:
                EnableGameOver(_byForced);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_newState), _newState, null);
        }

        OnAfterStateChanged?.Invoke(_newState);
    }

    #endregion

    #region PRIVATE_METHODS
    private IEnumerator EnableHome(bool _byForced)
    {
        yield return new WaitForEndOfFrame();
       
    }
    private void EnableGamePlay(bool _byForced)
    {
    }
    private void EnableTutorial(bool _byForced)
    {
    }
  
    private void EnableGameOver(bool _byForced)
    {
    }

    #endregion
   
  
  
}