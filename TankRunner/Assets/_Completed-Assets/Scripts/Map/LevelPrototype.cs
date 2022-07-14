using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class LevelPrototype : ScriptableObject
{
    public List<ObstaclePrototype> obstacles;
    public string levelName;
    public int LevelDistanceUnit;
}
