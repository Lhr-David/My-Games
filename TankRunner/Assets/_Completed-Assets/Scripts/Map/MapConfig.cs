using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public List<LevelPrototype> levels;

    public float distancePerUnit;
    public float xOffsetDelta;
    public float obstacleOffset;

    public List<ObstaclePrototype> obstacles;
}
