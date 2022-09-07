using UnityEngine;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;
    public Transform cameraToShake;

    public int coin = 0;
    public int hpMax = 3;
    public int hp { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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
}
