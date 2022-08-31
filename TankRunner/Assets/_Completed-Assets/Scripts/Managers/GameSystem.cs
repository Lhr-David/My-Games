using UnityEngine;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;
    public Transform cameraToShake;

    private void Awake()
    {
        instance = this;
    }

    public void DamagePlayer(int damage)
    {
        GameHudBehaviour.instance.BlinkVignette();
        cameraToShake.DOShakePosition(0.4f, 1, 6);
    }
}
