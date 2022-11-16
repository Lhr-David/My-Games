using UnityEngine;
using com;

public class WinScorePanelBehaviour : MonoBehaviour
{
    public ParticleSystem ps;
    public int multiplier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WinBullet")
        {
            ps.Play();
            ChargeSystem.instance.multiplier = multiplier;
            SoundSystem.instance.Play("scorePanel");
        }
    }
}
