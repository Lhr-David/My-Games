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
    }

    public float GetHpRatio()
    {
        return (float)hp / hpMax;
    }

    public void DamagePlayer(int damage)
    {
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
        tankMovement.forceStop = true;
        GroundSystem.instance.StopTimer();
        tankShooting.shootState = TankShooting.ShootState.Disabled;
        nextButton.SetActive(false);
        hpBarView.SetActive(false);
        ChargeSystem.instance.Show();
        ChargeSystem.instance.multiplier = 1;
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
}
