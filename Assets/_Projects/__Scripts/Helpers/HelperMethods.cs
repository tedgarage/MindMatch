using DG.Tweening;
using TMPro;
using UnityEngine;

public static class HelperMethods
{


    public static Sequence OpenRay(Transform _raysParent)
    {
        Sequence _raySeq = DOTween.Sequence();

        float duration = 0.5f;
        _raySeq.Append(_raysParent.DOScale(1, duration).SetEase(Ease.OutSine));

        //_portalSeq.Join(DOVirtual.Float(1.2f, 0f, duration, val =>
        //{
        //    portalRays.material.SetFloat("_Dissolve", val);

        //}).SetEase(Ease.OutSine));

        return _raySeq;
    }

    public static Sequence CloseRay(Transform _raysParent, float _duration = 0.5f)
    {
        Sequence _raySeq = DOTween.Sequence();

        _raySeq.Append(_raysParent.DOScale(Vector3.up, _duration).SetEase(Ease.OutSine));

        return _raySeq;
    }
    public static Sequence Blink(Material currentMat, float finalValue, float _duration = 0.28f)
    {
        float blinkTime = _duration / 4f;
        Sequence blinkSeq = DOTween.Sequence();
        Ease outEase = Ease.OutSine;
        Ease inEase = Ease.InSine;

        blinkSeq.Append(currentMat.DOFade(finalValue * 0.25f, blinkTime).SetEase(outEase));
        blinkSeq.Append(currentMat.DOFade(finalValue * 0.1f, blinkTime / 2).SetEase(inEase));
        blinkSeq.Append(currentMat.DOFade(finalValue * 0.5f, blinkTime / 2).SetEase(outEase));
        blinkSeq.Append(currentMat.DOFade(finalValue * 0.1f, blinkTime / 2).SetEase(inEase));
        blinkSeq.Append(currentMat.DOFade(finalValue * 0.75f, blinkTime / 2).SetEase(outEase));
        blinkSeq.Append(currentMat.DOFade(finalValue * 0.1f, blinkTime / 2).SetEase(inEase));
        blinkSeq.Append(currentMat.DOFade(finalValue, blinkTime / 2).SetEase(outEase));

        return blinkSeq;

    }
    public static Sequence Blink(TextMeshPro text, float finalValue, float _duration = 0.28f)
    {
        float blinkTime = _duration / 4f;
        Sequence blinkSeq = DOTween.Sequence();
        Ease outEase = Ease.OutSine;
        Ease inEase = Ease.InSine;

        blinkSeq.Append(text.DOFade(finalValue * 0.25f, blinkTime).SetEase(outEase));
        blinkSeq.Append(text.DOFade(finalValue * 0.1f, blinkTime / 2).SetEase(inEase));
        blinkSeq.Append(text.DOFade(finalValue * 0.5f, blinkTime / 2).SetEase(outEase));
        blinkSeq.Append(text.DOFade(finalValue * 0.1f, blinkTime / 2).SetEase(inEase));
        blinkSeq.Append(text.DOFade(finalValue * 0.75f, blinkTime / 2).SetEase(outEase));
        blinkSeq.Append(text.DOFade(finalValue * 0.1f, blinkTime / 2).SetEase(inEase));
        blinkSeq.Append(text.DOFade(finalValue, blinkTime / 2).SetEase(outEase));

        return blinkSeq;

    }
}

public static class CustomZeroVector
{
    public static Vector3 X { get { return new Vector3(0, 1, 1); } }
    public static Vector3 Y { get { return new Vector3(1, 0, 1); } }
    public static Vector3 Z { get { return new Vector3(1, 1, 0); } }
}