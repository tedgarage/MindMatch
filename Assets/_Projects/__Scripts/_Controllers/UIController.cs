using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameScreenUIHandler gameScreenUIHandler;
    [SerializeField] private HomeScreenUIHandler homeScreenUIHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public void OpenHome()
    {
        // homeScreenUIHandler.OpenHome();
    }
    public void StartGame()
    {
        // homeScreenUIHandler.CloseHome();
        // gameScreenUIHandler.StartGame();
    }
    public void HomeWithGameOver()
    {
        
    }
    public void InitHomeWithGameComplete()
    {
        
    }
}
