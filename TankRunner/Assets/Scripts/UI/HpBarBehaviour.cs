using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HpBarBehaviour : MonoBehaviour
{
    public Image mask;

    public void SetValue(float ratio)
    {
        mask.DOKill();
        mask.DOFillAmount(ratio, 0.3f);
        //mask.fillAmount = ratio;
    }
}