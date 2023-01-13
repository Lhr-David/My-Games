using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public Transform cameraToShake;

    public int coin = 0;
    public int hpMax = 3;
    public int hp { get; private set; }
    public TankMovement tankMovement;
    public TankShooting tankShooting;

    public CanvasGroup winCg;
    public CanvasGroup looseCg;

    public GameObject coinView;
    public GameObject hpBarView;
    public GameObject chargeView;
    public GameObject nextButton;
    public WinPanelBehaviour winPanel;
    public CanvasGroup cgStartGame;

    public GameObject shieldView;
    public bool hasShield;
    public int shieldDestroyCount = 1;
    int _shieldDestroyCounter;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tankMovement.forceStop = true;

        if (LevelPicker.currentLevelIndex > 0)
            StartGame();
    }

    public void StartGame()
    {
        cgStartGame.DOFade(0, 0.5f);
        cgStartGame.blocksRaycasts = false;
        cgStartGame.interactable = false;
        SoundSystem.instance.Play("scorePanel");

        StartCoroutine(StartGameProcess());
    }

    IEnumerator StartGameProcess()
    {
        yield return new WaitForSeconds(0.1f);
        MapSystem.instance.StartLevel(LevelPicker.currentLevelIndex);
        GroundSystem.instance.StartTimer();

        hp = hpMax;

        yield return new WaitForSeconds(0.4f);
        GameHudBehaviour.instance.SyncCoin();
        GameHudBehaviour.instance.SyncHp();
        tankShooting.shootState = TankShooting.ShootState.Normal;
        tankMovement.forceStop = false;

        //Debug.Log("PopupShootTip " + LevelPicker.currentLevelIndex);
        if (LevelPicker.currentLevelIndex == 2)
        {
            yield return new WaitForSeconds(0.4f);
            PopupShootTip("Dodge the turret!");
        }
        if (LevelPicker.currentLevelIndex == 1)
        {
            yield return new WaitForSeconds(0.4f);
            PopupShootTip("Shoot: Swipe up");
        }
        if (LevelPicker.currentLevelIndex == 0)
        {
            yield return new WaitForSeconds(0.4f);
            PopupShootTip("Move left: Swipe left\nMove right: Swipe right");
        }
    }

    public float GetHpRatio()
    {
        return (float)hp / hpMax;
    }

    public void ToggleShield(bool onOff, bool hasAnim)
    {
        //public GameObject shieldView;
        //public bool hasShield;
        //public int shieldDestroyCount = 1;
        // int _shieldDestroyCounter;
        hasShield = onOff;
        if (onOff)
            _shieldDestroyCounter = shieldDestroyCount;

        if (onOff)
        {
            shieldView.SetActive(true);
            if (hasAnim)
            {
                shieldView.transform.localScale = Vector3.zero;
                shieldView.transform.DOScale(1, 0.6f).SetEase(Ease.OutBack);
            }
            else
            {
                shieldView.transform.localScale = Vector3.one;
            }
        }
        else
        {
            if (hasAnim)
            {
                shieldView.transform.DOScale(0, 0.5f).OnComplete(() => { shieldView.SetActive(false); });
            }
            else
            {
                shieldView.SetActive(false);
            }
        }
    }

    public void HealPlayer(int amount)
    {
        hp += amount;
        if (hp > hpMax)
            hp = hpMax;

        GameHudBehaviour.instance.SyncHp();
    }

    public void DamagePlayer(int damage)
    {
        if (hasShield)
        {
            _shieldDestroyCounter -= 1;
            if (_shieldDestroyCounter <= 0)
            {
                ToggleShield(false, true);
            }
            SoundSystem.instance.Play("deflect");
            return;
        }

        GameHudBehaviour.instance.BlinkVignette();
        cameraToShake.DOShakePosition(0.4f, 1, 6);
        hp -= damage;
        GameHudBehaviour.instance.SyncHp();
        if (hp < 0)
        {
            Loose();
        }
    }

    public void AddCoin(int i)
    {
        coin += i;
        GameHudBehaviour.instance.SyncCoin();
        if (i > 0)
        {
            GameHudBehaviour.instance.PunchCoinAnim();
        }
    }

    public void Win()
    {
        Debug.Log("win1");
        ToggleShield(false, true);
        tankMovement.forceStop = true;
        GroundSystem.instance.StopTimer();
        tankShooting.shootState = TankShooting.ShootState.Disabled;
        nextButton.SetActive(false);
        hpBarView.SetActive(false);
        ChargeSystem.instance.Show();
        ChargeSystem.instance.multiplier = 1;

        if (LevelPicker.currentLevelIndex == 0)
        {
            PopupShootTip("Try swipe up when the bar is full");
        }
    }

    public void WinTest()
    {
        Debug.Log("win2");
        LevelPicker.OnWin();
        winPanel.Reset();

        winCg.DOFade(1, 3).OnComplete(() =>
        {
            winPanel.Setup();
            winCg.blocksRaycasts = true;
            winCg.interactable = true;
        });
    }

    public void Loose()
    {
        tankShooting.shootState = TankShooting.ShootState.Disabled;
        Debug.Log("Loose");
        tankMovement.forceStop = true;
        GroundSystem.instance.StopTimer();
        LevelPicker.OnFail();
        looseCg.DOFade(1, 3).OnComplete(ReloadScene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickNextLevelButton()
    {
        SceneManager.LoadScene(0);
    }

    public GameObject shootTip;
    public RectTransform shootTipPanel;
    public TMPro.TextMeshProUGUI text;

    public void PopupShootTip(string s)
    {
        text.text = s;
        shootTip.SetActive(true);
        shootTipPanel.DOKill();
        shootTipPanel.localScale = Vector3.one * 0.5f;
        shootTipPanel.DOScale(1, 0.35f).OnComplete(() => { Time.timeScale = 0; });
        SoundSystem.instance.Play("deflect");
    }

    public void HideShootTip()
    {
        shootTip.SetActive(false);
        SoundSystem.instance.Play("scorePanel");
        Time.timeScale = 1;
    }
}
