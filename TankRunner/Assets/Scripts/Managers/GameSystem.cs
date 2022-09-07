using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;
    public Transform cameraToShake;

    public int coin = 0;
    public int hpMax = 3;
    public int hp { get; private set; }
    public TankMovement tankMovement;
    public CanvasGroup winCg;
    public CanvasGroup looseCg;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MapSystem.instance.StartLevel(0);
        GroundSystem.instance.StartTimer();

        hp = hpMax;
        GameHudBehaviour.instance.SyncCoin();
        GameHudBehaviour.instance.SyncHp();
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
        Debug.Log("win");
        tankMovement.forceStop = true;
        GroundSystem.instance.StopTimer();

        winCg.DOFade(1, 3).OnComplete(ReloadScene);
    }

    public void Loose()
    {
        Debug.Log("Loose");
        tankMovement.forceStop = true;
        GroundSystem.instance.StopTimer();

        looseCg.DOFade(1, 3).OnComplete(ReloadScene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
