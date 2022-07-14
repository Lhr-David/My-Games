using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public static MapSystem instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
