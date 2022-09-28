using UnityEngine;
using DG.Tweening;

public class ChargeSystem : MonoBehaviour
{
    public static ChargeSystem instance;
    public CanvasGroup cg;
    public GameObject text;
    public float chargeBarHeight;
    public float chargeDurationUp;
    public float chargeDurationDown;
    public RectTransform bar;
    Vector2 _sizeDeltaMin;
    Vector2 _sizeDeltaMax;
    public CameraFollowZOnlyBehaviour cam;
    public Transform camTargetTrans1;
    public Transform camTargetTrans2;
    public Transform camTrans1;
    public Transform camTrans2;
    public Transform winBullet;

    private void Awake()
    {
        instance = this;
        cg.alpha = 0;
    }

    private void Start()
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
        GoUpAnim();
        GameSystem.instance.tankShooting.shootState = TankShooting.ShootState.Charge;
    }

    public float ShowFiredInformationAndGetFireValue()
    {
        text.SetActive(false);
        bar.DOKill();
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
}
