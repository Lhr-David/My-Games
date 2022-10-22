using UnityEngine;
using DG.Tweening;

public class ChargeSystem : MonoBehaviour
{
    public static ChargeSystem instance;
    public CanvasGroup cg;
    public GameObject text;
    public float chargeDurationUp;
    public float chargeDurationDown;
    public RectTransform bar;
    Vector2 _sizeDeltaMin;
    Vector2 _sizeDeltaMax;
    Vector2 _sizeDeltaFiring;
    public CameraFollowZOnlyBehaviour cam;
    public Transform camTargetTrans1;
    public Transform camTargetTrans2;
    public Transform camTrans1;
    public Transform camTrans2;
    public Transform winBullet;

    public WinScorePanelBehaviour winScorePanel50;
    public WinScorePanelBehaviour winScorePanel20;
    public WinScorePanelBehaviour winScorePanel10;

    public int multiplier;

    private void Awake()
    {
        instance = this;
        cg.alpha = 0;
    }

    public void Init(float chargeBarHeight)
    {
        var size = bar.sizeDelta;
        _sizeDeltaMin = size;
        _sizeDeltaMin.y = 0;
        _sizeDeltaMax = size;
        _sizeDeltaMax.y = chargeBarHeight;
    }

    public void Show()
    {
        cg.DOFade(1, 1).OnComplete(ShowChargeInformation);

        bar.DOKill();
        bar.sizeDelta = _sizeDeltaMin;
        text.SetActive(false);

        cam.enabled = false;
        camTrans1.DOKill();
        camTrans2.DOKill();
        camTrans1.DOMove(camTargetTrans1.position, 1.2f).SetEase(Ease.OutCubic);
        camTrans2.DORotate(camTargetTrans1.eulerAngles, 1.2f).SetEase(Ease.OutCubic);
    }

    public void ShowChargeInformation()
    {
        text.SetActive(true);
        GameHudBehaviour.instance.SetLevelTitle("");
        GoUpAnim();
        GameSystem.instance.tankShooting.shootState = TankShooting.ShootState.Charge;
    }

    public float ShowFiredInformationAndGetFireValue()
    {
        text.SetActive(false);
        bar.DOKill();
        _sizeDeltaFiring = bar.sizeDelta;
        float fireValue = ((float)bar.sizeDelta.y) / _sizeDeltaMax.y;
        GameSystem.instance.tankShooting.shootState = TankShooting.ShootState.Disabled;
        return fireValue;
    }

    void GoUpAnim()
    {
        bar.DOSizeDelta(_sizeDeltaMax, chargeDurationUp).SetEase(Ease.InQuad).OnComplete(GoDownAnim);
    }

    void GoDownAnim()
    {
        bar.DOSizeDelta(_sizeDeltaMin, chargeDurationDown).SetEase(Ease.OutQuad).OnComplete(GoUpAnim);
    }

    public void SetCameraFollowWinBullet(Transform bullet)
    {
        winBullet = bullet;
        cam.target = bullet;
        cam.ResetOffset(camTargetTrans2);
        cam.enabled = true;
        GroundSystem.instance.StartWinBulletTimer();
    }

    public void SyncChargeBarByWinBullet(float ratio)
    {
        if (ratio >= 1)
        {
            bar.sizeDelta = new Vector2(_sizeDeltaFiring.x, 0);
        }
        else
        {
            bar.sizeDelta = new Vector2(_sizeDeltaFiring.x, _sizeDeltaFiring.y * (1 - ratio));
        }
    }

    public void SetupAndShowWinPanels(float dist10, float dist20, float dist50)
    {
        winScorePanel50.gameObject.transform.position = new Vector3(0, 2.5f, dist50);
        winScorePanel20.gameObject.transform.position = new Vector3(0, 2.5f, dist20);
        winScorePanel10.gameObject.transform.position = new Vector3(0, 2.5f, dist10);

        winScorePanel50.gameObject.SetActive(true);
        winScorePanel20.gameObject.SetActive(true);
        winScorePanel10.gameObject.SetActive(true);
    }

    public void HideWinPanels()
    {
        winScorePanel50.gameObject.SetActive(false);
        winScorePanel20.gameObject.SetActive(false);
        winScorePanel10.gameObject.SetActive(false);
    }
}
