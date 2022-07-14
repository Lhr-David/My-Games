using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public List<LevelPrototype> obstacles;

    public float distancePerUnit;

    public float GetDistance(int u)
    {
        return distancePerUnit * u;
    }
}
