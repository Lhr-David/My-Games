using UnityEngine;
using com;

public class PickupBehaviour : MonoBehaviour
{
    public enum PickupTypes
    {
        None = 0,
        Invinsible = 1,
        Heal = 2,
        Magnet = 3,
    }

    public PickupTypes pickupType;

    public GameObject vfxPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var vfx = Instantiate(vfxPrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(vfx, 3);

            SoundSystem.instance.Play("pickup");

            GainEffect();
            Destroy(gameObject);
        }
    }

    void GainEffect()
    {
        switch (pickupType)
        {
            case PickupTypes.None:
                break;
            case PickupTypes.Invinsible:
                GameSystem.instance.ToggleShield(true, true);
                break;
            case PickupTypes.Heal:
                GameSystem.instance.HealPlayer(3);
                break;
            case PickupTypes.Magnet:
                GameSystem.instance.tankMovement.hasMagnet = true;
                break;
        }
    }
}