using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class HpBarBehaviour : MonoBehaviour
{
    public float barLength;
    public RectTransform bar;
    public Image maskSizeRef;
    public RectTransform barContent;

    public void Start()
    {
        RectTransform t = maskSizeRef.GetComponent<RectTransform>();
        barLength = maskSizeRef.preferredWidth;
        barLength = (float)t.rect.height * (float)maskSizeRef.preferredWidth / (float)maskSizeRef.preferredHeight;
        //var barHeight = maskSizeRef.preferredHeight;
        var barHeight = t.rect.height;
        var s = bar.sizeDelta;
        s.x = barLength;
        bar.sizeDelta = s;

        barContent.sizeDelta = new Vector2(barLength, barHeight);
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
    }

    public void SetValue(float ratio)
    {
        var s = bar.sizeDelta;
        s.x = barLength * ratio;
        bar.DOKill();
        bar.DOSizeDelta(s, 0.5f);
    }
}