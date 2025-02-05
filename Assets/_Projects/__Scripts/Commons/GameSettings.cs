using Jambav.Utilities;

public class GameSettings : Singleton<GameSettings>
{
   public int GameDuration  =  60;
   public int TickingSeconds ;
   public GameState initialState = GameState.Home;
   public bool tutorialEnable;
   
    
}
