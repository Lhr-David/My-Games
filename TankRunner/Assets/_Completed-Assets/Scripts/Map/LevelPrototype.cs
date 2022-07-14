using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class LevelPrototype : ScriptableObject
{
    public List<ObstacleData> obstacles;
    public string levelName;
    public int LevelLengthUnit;
}
