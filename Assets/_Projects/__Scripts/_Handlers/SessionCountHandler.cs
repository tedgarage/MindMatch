using UnityEngine;
using System;
using TMPro;
public class SessionCountHandler : MonoBehaviour
{
    public Action PortSelected;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI sessionCountText;
    [SerializeField] TextMeshProUGUI resetSessionButtonText;
    [SerializeField] TextMeshProUGUI highScoreCountText;
    [SerializeField] TextMeshProUGUI resetScoreButtonText;
    
    bool confirmResetSession;
    bool confirmResetScore;
    void Start()
    {

    }
    public void ShowPanel()
    {
        
        sessionCountText.text = GetSessionCount().ToString();
        highScoreCountText.text = DataManager.sharedInstance.GetBestScoreData().ToString();
        ResetButton(resetSessionButtonText,ref confirmResetSession);
        ResetButton(resetScoreButtonText, ref confirmResetScore);

    }
    public void Hide()
    {
        panel.SetActive(false);
    }
    public void ToggleView(){

        panel.SetActive(!panel.activeSelf);
        if(panel.activeSelf)
            ShowPanel();
    }

    public void ResetSessionCountButtonPressed()
    {
        if(confirmResetSession)
        {
            PlayerPrefs.SetInt(Constants.SESSION_COUNT_KEY, 0);
            PlayerPrefs.Save();
            sessionCountText.text = GetSessionCount().ToString();
            ResetButton(resetSessionButtonText,ref confirmResetSession);
        }
        else
        {
            ChangeAsConfirmButton(resetSessionButtonText,ref confirmResetSession);
        }
        

    }
    public void ResetHighScoreButtonPressed()
    {
       if(confirmResetScore)
        {
            PlayerPrefs.SetInt(Constants.BEST_SCORE_KEY, 0);
            PlayerPrefs.Save();
            highScoreCountText.text = DataManager.sharedInstance.GetBestScoreData().ToString();
            ResetButton(resetScoreButtonText, ref confirmResetScore);
        }
        else
        {
            ChangeAsConfirmButton(resetScoreButtonText,ref confirmResetScore);
        }

    }
    private int GetSessionCount()
    {
        if (PlayerPrefs.HasKey(Constants.SESSION_COUNT_KEY))
            return PlayerPrefs.GetInt(Constants.SESSION_COUNT_KEY);
        else
            return 0;
    }
    private void ResetButton(TextMeshProUGUI _textMeshProUGUI,ref bool _confirmReset)
    {
         _textMeshProUGUI.text  = "Reset";
        _confirmReset = false;
    }
     private void ChangeAsConfirmButton(TextMeshProUGUI _textMeshProUGUI,ref bool _confirmReset)
    {
         _textMeshProUGUI.text  = "Confirm?";
        _confirmReset = true;
    }
    public void AddSessionCount()
    {
        PlayerPrefs.SetInt(Constants.SESSION_COUNT_KEY, GetSessionCount() + 1);
        PlayerPrefs.Save();
    }
}