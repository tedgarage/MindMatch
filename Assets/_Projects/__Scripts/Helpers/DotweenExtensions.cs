using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public static class DoTweenExtensions
{

    #region CANVAS GROUP

    public static Sequence BlinkAnimation(this CanvasGroup canvasGroup, float _delay = 0.0f)
    {
        Reset();

        float time = 0.1f;

        Ease outEase = Ease.OutSine;
        Ease inEase = Ease.InSine;

        Sequence seq = DOTween.Sequence();

        Sequence blinkSeq = DOTween.Sequence();
        blinkSeq.AppendInterval(_delay);
        blinkSeq.Append(canvasGroup.DOFade(1, time).SetEase(outEase));
        blinkSeq.Append(canvasGroup.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(canvasGroup.DOFade(1f, time / 2).SetEase(outEase));
        blinkSeq.Append(canvasGroup.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(canvasGroup.DOFade(1, time / 2).SetEase(outEase));
        blinkSeq.Append(canvasGroup.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(canvasGroup.DOFade(1, time / 2).SetEase(outEase));

        seq.Append(blinkSeq);

        return seq;

        void Reset()
        {
            //Reset
            canvasGroup.alpha = 0;
        }
    }

    public static Sequence FlickerOpenAnimation(this CanvasGroup canvasGroup, bool scale = false, float time = 0.075f)
    {
        //float time = 0.07f;

        Ease linear = Ease.Linear;

        Vector3 initScale = canvasGroup.transform.localScale;

        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            Reset();
        });

        Sequence blinkSeq = DOTween.Sequence();

        blinkSeq.Append(canvasGroup.DOFade(1, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0.5f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0f, time).SetEase(linear));
        //blinkSeq.Append(canvasGroup.DOFade(1f, time).SetEase(linear));
        //blinkSeq.Append(canvasGroup.DOFade(0.25f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(1f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0.2f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(1f, time).SetEase(linear));

        seq.Append(blinkSeq);
        if (scale)
            seq.Join(canvasGroup.transform.DOScale(initScale, time * 5).SetEase(Ease.OutSine));


        seq.OnComplete(() =>
        {
            canvasGroup.alpha = 1;
        });

        return seq;

        void Reset()
        {
            //Reset
            canvasGroup.alpha = 0;
            if (scale)
                canvasGroup.transform.localScale = initScale * 1.2f;
        }
    }

    public static Sequence FlickerCloseAnimation(this CanvasGroup canvasGroup, float time = 0.075f)
    {
        //float time = 0.07f;

        Ease linear = Ease.Linear;

        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            Reset();
        });

        Sequence blinkSeq = DOTween.Sequence();

        blinkSeq.Append(canvasGroup.DOFade(0, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0.5f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(1f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0.5f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0.6f, time).SetEase(linear));
        blinkSeq.Append(canvasGroup.DOFade(0f, time).SetEase(linear));

        seq.Append(blinkSeq);

        return seq;

        void Reset()
        {
            //canvasGroup.alpha = 1;
        }
    }

    #endregion

    #region TEXT ANIMATIONS
    public static Sequence BlinkAnimation(this TMP_Text currentText)
    {
        Reset();

        float time = 0.075f;

        Ease outEase = Ease.OutSine;
        Ease inEase = Ease.InSine;

        Sequence seq = DOTween.Sequence();

        Sequence blinkSeq = DOTween.Sequence();

        blinkSeq.Append(currentText.DOFade(1, time).SetEase(outEase));
        blinkSeq.Append(currentText.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(currentText.DOFade(1f, time / 2).SetEase(outEase));
        blinkSeq.Append(currentText.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(currentText.DOFade(1, time / 2).SetEase(outEase));
        blinkSeq.Append(currentText.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(currentText.DOFade(1, time / 2).SetEase(outEase));

        seq.Append(blinkSeq);

        return seq;

        void Reset()
        {
            //Reset
            currentText.DOFade(0, 0);
        }
    }
    #endregion


    #region IMAGE ANIMATIONS

    public static Sequence BlinkAnimation(this Image currentText)
    {
        Reset();

        float time = 0.07f;

        Ease outEase = Ease.OutSine;
        Ease inEase = Ease.InSine;

        Sequence seq = DOTween.Sequence();

        Sequence blinkSeq = DOTween.Sequence();

        blinkSeq.Append(currentText.DOFade(1, time).SetEase(outEase));
        blinkSeq.Append(currentText.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(currentText.DOFade(1f, time / 2).SetEase(outEase));
        blinkSeq.Append(currentText.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(currentText.DOFade(1, time / 2).SetEase(outEase));
        blinkSeq.Append(currentText.DOFade(0.5f, time / 2).SetEase(inEase));
        blinkSeq.Append(currentText.DOFade(1, time / 2).SetEase(outEase));

        seq.Append(blinkSeq);
        seq.Join(currentText.transform.DOScale(1, time * 5).SetEase(Ease.OutSine));

        return seq;

        void Reset()
        {
            //Reset
            currentText.DOFade(0, 0);
            currentText.transform.DOScale(1.2f, 0);
        }
    }
    #endregion
}