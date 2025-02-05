using UnityEngine;
using System.Collections;
using TMPro;
[RequireComponent(typeof(TextMeshPro))]
public class FPSText : MonoBehaviour
{
    private TextMeshPro fpsText;
    private float deltaTime = 0;

    private void Awake()
    {
        fpsText = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}