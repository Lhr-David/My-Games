using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public string hitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            com.SoundSystem.instance.Play(hitSound);
            OnHitPlayer();
        }
    }

    public virtual void OnHitPlayer()
    {

    }
}
