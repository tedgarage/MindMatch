using System.Data;
using UnityEngine.UIElements;

public static class Constants
{
    #region Score
    public const int SCORE_MULTIPLIER = 100;
    public const int CORRECT_ANSWER_ON_TIME = 10;
    public const int CORRECT_ANSWER_ON_TIME_WRONG_DIRECTION = 5;
    public const int CORRECT_ANSWER_AFTER_DISMISS = 5;
    public const float THREE_STAR_MULTIPLIER = 2;
    public const float THREE_STAR_REMAINING_TIME_PERCENTAGE = 70;
    public const float TWO_STAR_MULTIPLIER = 1.5f;
    public const float TWO_STAR_REMAINING_TIME_PERCENTAGE = 30;
    public const float ONE_STAR_MULTIPLIER = 1;

    public const float STREAK_DURATION_SEC = 6f;
    public const string BEST_SCORE_KEY = "BestScore" ;
    public const string SESSION_COUNT_KEY = "SessionCount" ;
    public const string PORT_NAME_KEY = "PortName" ;
    public const string USING_RED_BOX_KEY = "RedBox";
    public const string USING_BLUE_BOX_KEY = "BlueBox";
    public const string USING_GREEN_BOX_KEY = "GreenBox";
    public const string USING_YELLOW_BOX_KEY = "YellowBox";
    
    #endregion


    #region Tutorial
    public const string FIND_THE_PRODUCT_TUTORIAL_TEXT = "<size>Grab<size> the  colored Blok(s)./n <size>Match<size> the product./n <size>Place<size> it top-up in the slot!";
    public const string TOTAL_TIME_TUTORIAL_TEXT = "You have  {0} minute. Good luck!";
    #endregion

    #region GamePlay
    public const float MAX_TIME_PRODUCT_TIME = 20;
    public const float MIN_PRODUCT_TIME = 10;
     public const float DECREASE_TIME_RANGE = .2f;

    public const int DIFFICULTY_LEVEL_ONE_RANGE = 5;
    public const int DIFFICULTY_LEVEL_TWO_RANGE = 10;

    #endregion


    #region Resource
    public const string PRODUCT_LOGO_PATH = "ProductSprites/Zoho_{0}";

    #endregion


    #region Product
    public const string PRODUCT_NAME = "Zoho {0}";

    #endregion
    #region Game play
    public const string START_TEXT_COUNTDOWN = "Let's Go!";
    #endregion

    #region Game End
    public const string GAME_OVER_TEXT = "Times Up!";
    public const string GAME_COMPLETE_TEXT = "Game Completed!";



    #endregion




}