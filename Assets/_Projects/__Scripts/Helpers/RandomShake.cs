using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomShake : MonoBehaviour
{
    Tween mainSeq;
    public Vector3 strength;
    public float duration;

    void Start()
    {
        SetSeq();
    }

    public void SetSeq()
    {
        mainSeq = transform.DOShakePosition(duration, strength, 3, 90, false, false).OnComplete(() =>
        {
            SetSeq();
        });
    }

    public void StopMovement()
    {
        if (mainSeq != null && mainSeq.IsActive())
        {
            mainSeq.Pause();
        }
    }

    public void StartMovement()
    {
        if (mainSeq != null && mainSeq.IsActive())
        {
            mainSeq.Play();
        }
    }

    //private void OnEnable()
    //{
    //    StartMovement();
    //}

    //private void OnDisable()
    //{
    //    StopMovement();
    //}
}
