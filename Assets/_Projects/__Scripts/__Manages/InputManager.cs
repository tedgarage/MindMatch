using Jambav.Utilities;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    #region  VARIABLES
    [SerializeField] private ZohoProduct zohoProduct;
    [SerializeField] private int direction;
    [SerializeField]
    #endregion

    #region UNITY_METHODS
    void Start()
    {

    }
    void Update()
    {
      
        // #if UNITY_EDITOR
        if (GameManager.sharedInstance.currentGameState == GameState.Home)
            ForHome();
        else if (GameManager.sharedInstance.currentGameState == GameState.GamePlay || GameManager.sharedInstance.currentGameState == GameState.Tutorial)
            ForGamePlay();
        else if (GameManager.sharedInstance.currentGameState == GameState.EndGame)
            ForGameEnd();

    }

    #endregion

    #region DEBUG METHODS
    // #if UNITY_EDITOR
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

    // #endif
    #endregion

}
