using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class FadeMatAnimation : MonoBehaviour
{
    Material mat;

    public void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        Sequence seq = DOTween.Sequence();
        seq.Append(mat.DOFade(0f, 1f));
        seq.Append(mat.DOFade(.1f, 1f));
        seq.SetLoops(-1);
    }
}
