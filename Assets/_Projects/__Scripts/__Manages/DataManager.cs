using Jambav.Utilities;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
  [SerializeField] ScoreHandler scoreHandler; 

    public string GetBestScoreData(){
       return scoreHandler.GetBestScore().ToString();
    }

  
}
