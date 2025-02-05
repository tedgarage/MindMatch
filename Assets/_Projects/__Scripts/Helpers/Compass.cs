using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Compass : MonoBehaviour
{
    private Transform lookAtObject;
    private Transform arrow;
    private bool look = false;

    private void Awake()
    {
        arrow = transform.GetChild(0);
    }

    void Update()
    {
        if (look)
        {
            arrow.LookAt(lookAtObject);
        }

    }
    public void SetLookAtObject(Transform _lookingObject)
    {
        lookAtObject = _lookingObject;
    }
    public void StartLooking()
    {

        look = true;
        arrow.gameObject.SetActive(true);
    }
    public void StopLooking()
    {
        arrow.gameObject.SetActive(false);
        lookAtObject = null;
        look = false;
    }
    public void ShowCompass()
    {
        transform.DOScale(1, 0.1f);
    }
    public void HideCompass()
    {
        transform.DOScale(0, 0.1f);
    }
}
