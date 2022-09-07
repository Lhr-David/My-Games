using UnityEngine;
using System.Collections;

public class HouseBehaviour : ObstacleBehaviour
{
    public ParticleSystem ps;
    public Transform psPos;

    public override void OnHitPlayer()
    {
        var go = Instantiate(ps, psPos.position, psPos.rotation, transform.parent);
        Destroy(go, 2.0f);

        GameSystem.instance.DamagePlayer(1);
        Destroy(gameObject);
    }
}
