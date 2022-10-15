using UnityEngine;
using DG.Tweening;

public class HouseBehaviour : ObstacleBehaviour
{
    public ParticleSystem psWound;
    public ParticleSystem psDie;
    public Transform psPos;
    public int hp = 1;

    public override void OnHitPlayer()
    {
        var go = Instantiate(psDie, psPos.position, psPos.rotation, transform.parent);
        Destroy(go, 2.0f);

        GameSystem.instance.DamagePlayer(1);
        Destroy(gameObject);
    }

    public override void OnHitBullet(GameObject bullet)
    {
        hp -= 1;
        if (hp <= 0)
        {
            var go = Instantiate(psDie, psPos.position, psPos.rotation, transform.parent);
            Destroy(go, 2.0f);
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
        else
        {
            var go = Instantiate(psWound, psPos.position, psPos.rotation, transform.parent);
            Destroy(go, 2.0f);
            Destroy(bullet.gameObject);
            //transform.DOShakePosition(0.25f, 0.8f, 10);
            transform.DOShakeRotation(0.25f, 10.8f, 10);
        }
    }
}
