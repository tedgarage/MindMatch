using System;
using System.Collections.Generic;
using DG.Tweening;
using Jambav.Utilities;
using TMPro;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private Transform ProductCardParent;
    [SerializeField] private Transform competitorCardParent;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TimerHandler timerHandler;

    List<ProductData> gameData;
    ProductCard selectedProduct;
    CompetitorCard selectedCompetitor;
    int levelIndex = 0;
    bool answerChecking;
    int answeredCount = 0;

    public void Start()
    {
        InputManager.sharedInstance.OnProductSelected += ProductSelected;
        InputManager.sharedInstance.OnCompetitorSelected += CompetitorSelected;
        InputManager.sharedInstance.OnTouchDetected += DetectTouch;

        InitGame();
        timerHandler.Init(GameEnd);
        timerHandler.SetText(timeText);

    }
    public void InitGame()
    {
        gameData = DataManager.sharedInstance.GetRandomDataForGame(ProductCardParent.childCount);
        for (int i = 0; i < ProductCardParent.childCount; i++)
        {
            gameData[i].CompetitorProductsList.Shuffle();
            ProductCardParent.GetChild(i).GetComponent<ProductCard>().Init(gameData[i].GetMainProductName(), gameData[i].TagLine);
        }
        StartLevel();
        RevealProducts();
    }
    public void StartLevel()
    {
        levelNumber.text = "Level " + (levelIndex + 1);
        infoText.text = "Level " + (levelIndex + 1) + "\nStart!";
        infoText.transform.parent.localScale = Vector3.zero;
        SetCompetitorData();
        DOTween.Sequence()
        .AppendInterval(levelIndex ==0?2f:1f)
        .AppendCallback(() =>
        {
            RevealCompetitors();
        })
        .AppendCallback(() =>
        {
            for (int i = 0; i < ProductCardParent.childCount; i++)
            {
                ProductCardParent.GetChild(i).GetComponent<ProductCard>().HideProduct();
            }
        })
        .AppendInterval(.5f)
        .AppendCallback(() =>
        {
            for (int i = 0; i < ProductCardParent.childCount; i++)
            {

                ProductCardParent.GetChild(i).GetComponent<ProductCard>().GameStart();
            }
            for (int i = 0; i < competitorCardParent.childCount; i++)
            {

                competitorCardParent.GetChild(i).GetComponent<CompetitorCard>().GameStart();
            }
        })
        .Append(infoText.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutBack))
        .AppendInterval(1)
        .Append(infoText.transform.parent.DOScale(0, 0.2f).SetEase(Ease.InBack))
        .AppendCallback(() =>
        {
            if (levelIndex == 0)
                timerHandler.StartTimer();
            else
                timerHandler.UnPauseTimer();
        });

    }

    private void SetCompetitorData()
    {
        List<int> indies = CommonMethods.GetShuffledIndiesList(ProductCardParent.childCount);
        for (int i = 0; i < competitorCardParent.childCount; i++)
        {
            competitorCardParent.GetChild(indies[i]).GetComponent<CompetitorCard>().Init(gameData[i].CompetitorProductsList[levelIndex], gameData[i].TagLine);
        }
    }

    public void RevealProducts()
    {
        for (int i = 0; i < ProductCardParent.childCount; i++)
        {
            ProductCardParent.GetChild(i).GetComponent<ProductCard>().RevealCard();
        }
    }
    public void RevealCompetitors()
    {
        for (int i = 0; i < competitorCardParent.childCount; i++)
        {
            competitorCardParent.GetChild(i).GetComponent<CompetitorCard>().RevealCard();
        }
    }

    private void CheckAnswer()
    {
        answerChecking = true;
        if (selectedProduct.GetTagLine().Equals(selectedCompetitor.GetTagLine()))
        {
            answeredCount++;

            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(.5f);
            sequence.AppendCallback(() =>
            {
                AudioManager.sharedInstance.PlayCorrectAnswerClip();
            });
            sequence.AppendInterval(1f);
            sequence.AppendCallback(() =>
            {
                selectedProduct.Answered();
                selectedCompetitor.Answered();
                selectedProduct = null;
                selectedCompetitor = null;
                for (int i = 0; i < ProductCardParent.childCount; i++)
                {
                    ProductCardParent.GetChild(i).GetComponent<ProductCard>().DisableBlackLayer();
                }
                for (int i = 0; i < competitorCardParent.childCount; i++)
                {
                    competitorCardParent.GetChild(i).GetComponent<CompetitorCard>().DisableBlackLayer();
                }
            });
            sequence.AppendInterval(.2f);
            sequence.AppendCallback(() =>
            {
                answerChecking = false;
                if (answeredCount == ProductCardParent.childCount)
                {
                    LevelDone();

                }

            });

        }
        else
        {

            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(.3f);
            sequence.AppendCallback(() =>
            {
                AudioManager.sharedInstance.PlayWrongAnswerClip();
            });
            sequence.AppendInterval(.5f);
            sequence.AppendCallback(() =>
            {
                selectedProduct.HideProduct();
                selectedProduct = null;
                selectedCompetitor.HideProduct();
                selectedCompetitor = null;

                for (int i = 0; i < ProductCardParent.childCount; i++)
                {
                    ProductCardParent.GetChild(i).GetComponent<ProductCard>().DisableBlackLayer();
                }
                for (int i = 0; i < competitorCardParent.childCount; i++)
                {

                    competitorCardParent.GetChild(i).GetComponent<CompetitorCard>().DisableBlackLayer();

                }
            });
            sequence.AppendInterval(.3f);
            sequence.AppendCallback(() =>
            {
                answerChecking = false;
            });


        }
    }

    private void LevelDone()
    {
        timerHandler.PauseTimer();
        answeredCount = 0;
        levelIndex++;
        bool gameComplete = false;
        if (levelIndex == 3)
            gameComplete = true;
        for (int i = 0; i < ProductCardParent.childCount; i++)
        {

            ProductCardParent.GetChild(i).GetComponent<ProductCard>().LevelDone();
        }
        infoText.text = gameComplete ? "Game Complete!" : "Level " + levelIndex + "\nDone!";
        infoText.transform.parent.localScale = Vector3.zero;
        DOTween.Sequence()
        .Append(infoText.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutBack))
        .AppendInterval(1)
        .Append(infoText.transform.parent.DOScale(0, 0.2f).SetEase(Ease.InBack))
        .AppendInterval(0.4f).AppendCallback(() =>
        {
            if (!gameComplete)
                StartLevel();

        });


    }
    private void DetectTouch()
    {
        print("Touch detected");
    }

    private void CompetitorSelected(CompetitorCard _obj)
    {
        if (answerChecking)
            return;
        if (selectedCompetitor != null)
        {
            selectedCompetitor.HideProduct();
        }
        selectedCompetitor = _obj;
        selectedCompetitor.ShowProduct();

        if (selectedProduct != null)
        {

            CheckAnswer();
        }

        for (int i = 0; i < competitorCardParent.childCount; i++)
        {
            if (competitorCardParent.GetChild(i).GetComponent<CompetitorCard>() != selectedCompetitor)
                competitorCardParent.GetChild(i).GetComponent<CompetitorCard>().EnableBlackLayer();
        }
    }

    private void ProductSelected(ProductCard _obj)
    {
        if (answerChecking)
            return;
        if (selectedProduct != null)
        {
            selectedProduct.HideProduct();
        }
        selectedProduct = _obj;
        selectedProduct.ShowProduct();
        if (selectedCompetitor != null)
        {
            CheckAnswer();
        }

        for (int i = 0; i < ProductCardParent.childCount; i++)
        {
            if (ProductCardParent.GetChild(i).GetComponent<ProductCard>() != selectedProduct)
                ProductCardParent.GetChild(i).GetComponent<ProductCard>().EnableBlackLayer();

        }

    }


    private void GameEnd()
    {
        print("Game over called");
        infoText.text = "GameOver";
        infoText.transform.parent.localScale = Vector3.zero;
        for (int i = 0; i < ProductCardParent.childCount; i++)
        {

            ProductCardParent.GetChild(i).GetComponent<ProductCard>().GameOver();
        }
        for (int i = 0; i < competitorCardParent.childCount; i++)
        {

            competitorCardParent.GetChild(i).GetComponent<CompetitorCard>().GameOver();
        }
        DOTween.Sequence()
        .Append(infoText.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutBack))
        .AppendInterval(0.4f).AppendCallback(() =>
        {


        });

        InputManager.sharedInstance.OnProductSelected -= ProductSelected;
        InputManager.sharedInstance.OnCompetitorSelected -= CompetitorSelected;
        InputManager.sharedInstance.OnTouchDetected -= DetectTouch;
    }
}