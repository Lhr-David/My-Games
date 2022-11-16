using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Text = TMPro.TextMeshProUGUI;

public class GameHudBehaviour : MonoBehaviour
{
    public static GameHudBehaviour instance;

    public Text coinText;
    public Text levelTitleText;

    public CanvasGroup cgVignette;

    public HpBarBehaviour hpBar;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cgVignette.alpha = 0;
        cgVignette.interactable = false;
        cgVignette.blocksRaycasts = false;
    }

    public void SetLevelTitle(string s)
    {
        levelTitleText.text = s;
    }

    public void SyncHp()
    {
        hpBar.SetValue(GameSystem.instance.GetHpRatio());
    }

    public void SyncCoin()
    {
        coinText.text = GameSystem.instance.coin + "";
    }

    public void PunchCoinAnim()
    {
        coinText.rectTransform.DOKill();
        coinText.rectTransform.localScale = Vector3.one;
        coinText.rectTransform.DOPunchScale(Vector3.one * 0.75f, 0.25f, 4, 0).OnComplete(
            () => { coinText.rectTransform.localScale = Vector3.one; });
    }

    public void BlinkVignette()
    {
        cgVignette.DOKill();
        cgVignette.DOFade(1, 0.2f).OnComplete(() => { cgVignette.DOFade(0, 0.15f); });
    }
}