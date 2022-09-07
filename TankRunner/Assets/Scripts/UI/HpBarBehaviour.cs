using UnityEngine;
using DG.Tweening;

public class HpBarBehaviour : MonoBehaviour
{
    public float barLength = 150;
    public RectTransform bar;

    public void SetValue(float ratio)
    {
        var s = bar.sizeDelta;
        s.x = barLength * ratio;
        bar.DOKill();
        bar.DOSizeDelta(s, 0.5f);
    }
}