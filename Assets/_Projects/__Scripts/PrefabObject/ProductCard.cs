using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProductCard : MonoBehaviour
{

    [SerializeField] private Transform objectHolder;
    [SerializeField] private Transform foregroundHolder;
    [SerializeField] private Transform productBackgroundHolder;
    [SerializeField] private Image logoImage;
    [SerializeField] private TextMeshProUGUI productText;
    [SerializeField] private Transform tagLineHolder;
    [SerializeField] private TextMeshProUGUI taglineText;
    [SerializeField] private Button button;
    [SerializeField] private Image blackLayer;
    [SerializeField] private Transform tickObject;


    private string productName;
    private string productTagLine;
    private Sequence cardSequence;
    private Sequence blackLayerSeq;
    bool answered = false;
    private bool gameDone;

    public void Start()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(string _productName, string _productTagLine)
    {
        answered = false;
        button.interactable = false;
        productName = _productName;
        productTagLine = _productTagLine;
        ResetCard();
        SetData();

    }
    public void RevealCard()
    {
        Reset();
        cardSequence = DOTween.Sequence();
        cardSequence.Append(objectHolder.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));

        void Reset()
        {
            ResetCardSeq();
            objectHolder.gameObject.SetActive(true);
            foregroundHolder.gameObject.SetActive(true);
            objectHolder.localScale = Vector3.zero;
        }

    }
    public void GameStart()
    {
        button.interactable = true;
    }
    public void LevelDone()
    {
         tickObject.DOScale(0,0.2f).SetEase(Ease.InBack).OnComplete(() =>{
            tickObject.gameObject.SetActive(false);
         });
    }
      public void GameOver()
    {
        button.interactable = false;
        gameDone = true;
    }

    public void UnRevealCard()
    {
         Reset();
        cardSequence = DOTween.Sequence();
        cardSequence.Append(objectHolder.DOScale(0, 0.25f).SetEase(Ease.OutBack));
        void Reset()
        {
            ResetCardSeq();
        }
    }
    public void ShowProduct()
    {

        button.interactable = false;
        ResetCardSeq();
        FlipCard(true);
        AudioManager.sharedInstance.PlayCardRevealClip();
    }
    public void HideProduct()
    {
        ResetCardSeq();
        FlipCard(false);
        print("Hide Product Called");
    }
    public void EnableBlackLayer()
    {
        ResetBlackLayerSeq();
        blackLayerSeq = DOTween.Sequence();
        blackLayerSeq.AppendCallback(() =>
        {
            blackLayer.gameObject.SetActive(true);
        });
        blackLayerSeq.Append(blackLayer.DOFade(.8f, 0.25f));
    }
    public void DisableBlackLayer()
    {
        ResetBlackLayerSeq();
        blackLayerSeq = DOTween.Sequence();
        blackLayerSeq.Append(blackLayer.DOFade(0f, 0.25f));
        blackLayerSeq.AppendCallback(() =>
        {
            blackLayer.gameObject.SetActive(false);
        });
    }
    private void ResetCardSeq(bool _complete = true)
    {
        if (cardSequence != null && cardSequence.IsActive())
        {
            if (_complete)
                cardSequence.Complete();
            else
                cardSequence.Kill();
            cardSequence = null;
        }
    }
    private void ResetBlackLayerSeq(bool _complete = true)
    {
        if (blackLayerSeq != null && blackLayerSeq.IsActive())
        {
            if (_complete)
                blackLayerSeq.Complete();
            else
                blackLayerSeq.Kill();
            blackLayerSeq = null;
        }
    }
    private void SetData()
    {
        logoImage.sprite = ResourcesManager.sharedInstance.GetProductSprite(productName);
        productText.text = "Zoho " + productName;
        taglineText.text = productTagLine;
    }
    public string GetTagLine()
    {
        return productTagLine;
    }
    private void ResetCard()
    {
        foregroundHolder.gameObject.SetActive(false);
        objectHolder.gameObject.SetActive(false);
        button.interactable = false;
        blackLayer.gameObject.SetActive(false);
        tickObject.gameObject.SetActive(false);
    }

    private void FlipCard(bool _show)
    {
        cardSequence = DOTween.Sequence();
        cardSequence.AppendCallback(() =>
        {
            if (_show)
            {
                tagLineHolder.localScale = Vector3.zero;
                if (!tagLineHolder.gameObject.activeSelf)
                    tagLineHolder.gameObject.SetActive(true);
            }
        });

        Sequence subSeq = DOTween.Sequence();
        subSeq.Append(objectHolder.DORotate(new Vector3(0, _show ? 90 : -90, 0), 0.2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        if (_show)
            subSeq.Join(blackLayer.DOFade(0, 0.1f));

        subSeq.AppendCallback(() =>
        {
            foregroundHolder.gameObject.SetActive(_show);
            productBackgroundHolder.gameObject.SetActive(!_show);
        });
        subSeq.Append(objectHolder.DORotate(new Vector3(0, _show ? -90 : 90, 0), 0.2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        if (_show)
            subSeq.Join(tagLineHolder.DOScale(1, 0.1f).SetEase(Ease.OutBack).SetDelay(.1f));

        cardSequence.Append(subSeq);

        if (!_show)
            cardSequence.Join(tagLineHolder.DOScale(0, 0.4f).SetEase(Ease.OutBack));

        if (!_show)
        {
            cardSequence.AppendCallback(() =>
            {
                button.interactable = true;
            });
        }

    }
    public void ButtonPressed()
    {
        if(gameDone)
            return;
            
        print("Button Pressed ");
        InputManager.sharedInstance.ProductSelected(this);
    }

    internal void Answered()
    {
        answered = true;
        button.interactable = false;
        tickObject.gameObject.SetActive(true);
        tickObject.localScale = Vector3.zero;

        tagLineHolder.DOScale(0, 0.2f).SetEase(Ease.OutBack);
        tickObject.DOScale(1,0.2f).SetEase(Ease.OutBack);

    }
}
