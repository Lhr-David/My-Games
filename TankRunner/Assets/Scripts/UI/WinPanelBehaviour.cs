using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class WinPanelBehaviour : MonoBehaviour
{
    public RectTransform rawCoinRect;
    public TMPro.TextMeshProUGUI rawCoin;
    public RectTransform finalCoinRect;
    public TMPro.TextMeshProUGUI finalCoin;
    public RectTransform multiplierRect;
    public TMPro.TextMeshProUGUI multiplier;
    public GameObject btn;

    public void Reset()
    {
        rawCoinRect.gameObject.SetActive(false);
        finalCoinRect.gameObject.SetActive(false);
        multiplierRect.gameObject.SetActive(false);
        btn.SetActive(false);
    }

    public void Setup()
    {
        StartCoroutine(SetupProcess());
    }

    IEnumerator SetupProcess()
    {

        yield return new WaitForSeconds(0.1f);

        rawCoinRect.gameObject.SetActive(true);
        rawCoin.text = "" + GameSystem.instance.coin;
        rawCoinRect.DOPunchScale(Vector3.one, 0.35f, 8, 0.6f);
        yield return new WaitForSeconds(0.4f);

        multiplierRect.gameObject.SetActive(true);
        multiplier.text = " x" + ChargeSystem.instance.multiplier+"!";
        multiplierRect.DOPunchScale(Vector3.one, 0.35f, 8, 0.6f);
        yield return new WaitForSeconds(0.6f);

        GameSystem.instance.coin = GameSystem.instance.coin * ChargeSystem.instance.multiplier;

        finalCoinRect.gameObject.SetActive(true);
        finalCoin.text = "" + GameSystem.instance.coin;
        finalCoinRect.DOPunchScale(Vector3.one, 0.35f, 8, 0.6f);
        yield return new WaitForSeconds(0.4f);
        btn.SetActive(true);
    }
}
