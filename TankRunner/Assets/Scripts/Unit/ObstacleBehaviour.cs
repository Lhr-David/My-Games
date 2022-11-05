using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public string hitSound;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            com.SoundSystem.instance.Play(hitSound);
            OnHitPlayer();
        }

        if (other.gameObject.tag == "Bullet")
        {
            com.SoundSystem.instance.Play(hitSound);
            OnHitBullet(other.gameObject);
        }
    }

    public virtual void OnHitPlayer()
    {

    }

    public virtual void OnHitBullet(GameObject bullet)
    {

    }
}
