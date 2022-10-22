using UnityEngine;

public class WinScorePanelBehaviour : MonoBehaviour
{
    public ParticleSystem ps;
    public int multiplier;

    public void ShowParticle()
    {
        ps.Play();
        com.SoundSystem.instance.Play("confetti");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WinBullet")
        {
            ShowParticle();
            ChargeSystem.instance.multiplier = multiplier;
        }
    }
}
