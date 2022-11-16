using UnityEngine;
using DG.Tweening;

public class CoinBehaviour : ObstacleBehaviour
{
    public MonoBehaviour[] componentsToDisable;
    public float height = 1;
    public ParticleSystem ps;
    public Transform psPos;
    public float speed = 14;

    bool _isAbsorbing;

    private void Update()
    {
        if (!_isAbsorbing && GameSystem.instance.tankMovement.hasMagnet)
        {
            var dist = (GameSystem.instance.tankMovement.transform.position - transform.position).magnitude;
            if (dist < 11)
            {
                _isAbsorbing = true;
            }

            return;
        }

        if (_isAbsorbing)
        {
            var dir = (GameSystem.instance.tankMovement.transform.position - transform.position);
            var newPos = transform.position + dir.normalized * Time.deltaTime * speed;
            transform.position = newPos;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            com.SoundSystem.instance.Play(hitSound);
            OnHitPlayer();
        }

        if (other.gameObject.tag == "Bullet")
        {
        }
    }


    public override void OnHitPlayer()
    {
        GameSystem.instance.AddCoin(1);

        var go = Instantiate(ps, psPos.position, psPos.rotation, transform.parent);
        Destroy(go, 2.0f);

        foreach (var c in componentsToDisable)
        {
            c.enabled = false;
        }

        float endY = transform.position.y + height;
        transform.DOMoveY(endY, 0.5f).SetEase(Ease.OutCubic).OnComplete(
            () =>
            {
                Destroy(this.gameObject);
            }
            );
    }
}
