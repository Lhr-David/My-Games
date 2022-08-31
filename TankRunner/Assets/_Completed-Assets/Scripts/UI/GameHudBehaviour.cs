using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameHudBehaviour : MonoBehaviour
{
    public static GameHudBehaviour instance;

    public int coin = 0;

    public Text coinText;

    public CanvasGroup cgVignette;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        SyncCoinNumber();
        cgVignette.alpha = 0;
        cgVignette.interactable = false;
        cgVignette.blocksRaycasts = false;
    }

    public void AddCoin(int i)
    {
        coin += i;
        SyncCoinNumber();

        if (i > 0)
        {
            coinText.DOKill();
            coinText.rectTransform.DOPunchScale(Vector3.one, 0.35f, 4, 0).OnComplete(
                () => { coinText.rectTransform.localScale = Vector3.one; });
        }
    }

    void SyncCoinNumber()
    {
        coinText.text = coin + "";
    }

    public void BlinkVignette()
    {
        cgVignette.DOKill();
        cgVignette.DOFade(1, 0.2f).OnComplete(() => { cgVignette.DOFade(0, 0.15f); });
    }
}
