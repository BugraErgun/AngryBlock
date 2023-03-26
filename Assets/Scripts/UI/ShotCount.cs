using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShotCount : MonoBehaviour
{
    public AnimationCurve scaleCurve;

    private CanvasGroup cg;

    private TextMeshProUGUI toptext, bottomtext;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        toptext = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        bottomtext = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        transform.localScale = Vector3.zero;
    }
    public void SetTopText(string text)
    {
        toptext.text = text;
    }
    public void SetBottomText(string text)
    {
        bottomtext.text = text;
    }
    public void Flash()
    {
        cg.alpha = 1;
        transform.localScale = Vector3.zero;
        StartCoroutine(FlashRoutine());
    }
    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 60; i++)
        {
            transform.localScale = Vector3.one * scaleCurve.Evaluate((float)i / 50);
            if (i>=40)
            {
                cg.alpha = (float)(60 - i) / 20;         
            }
            yield return null;
        }
        yield break;
    }
}
