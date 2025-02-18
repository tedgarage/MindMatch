using System;
using System.Collections.Generic;
using System.Linq;
using Jambav.Utilities;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
  [SerializeField] ScoreHandler scoreHandler; 
  [SerializeField] List<ProductData> gameData;


  public void Start()
  {
    ParseData();
  }

  public void ParseData(){
     List<Dictionary<string, object>> data = CSVReader.ReadFileFromResources(Constants.GAME_DATA_FILE_PATH);

      ZohoProduct product;
      List<string> competitors;
      string tagLine;

        for (int i = 0; i < data.Count; i++)
        {
          Dictionary<string,object> currentData = data[i];
          product = currentData[Constants.PRODUCT_KEY].ToString().ToEnum<ZohoProduct>();
          competitors = currentData[Constants.COMPETITORS_LIST_KEY].ToString().Split(',').ToList();
          tagLine = currentData[Constants.TAG_LINE_KEY].ToString();

          gameData.Add(new ProductData(product,competitors,tagLine));
        }

}
public List<ProductData> GetRandomDataForGame(int _numberOfProducts)
{
    if (_numberOfProducts <= 0 || _numberOfProducts > gameData.Count)
    {
        throw new ArgumentException("Invalid number of products requested.");
    }

    return gameData.OrderBy(x => UnityEngine.Random.value).Take(_numberOfProducts).ToList();

    // Use randomProducts as needed¯
}
    public string GetBestScoreData(){
       return scoreHandler.GetBestScore().ToString();
    }

  
}
