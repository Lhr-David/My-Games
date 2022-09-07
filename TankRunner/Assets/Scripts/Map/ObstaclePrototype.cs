using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class ObstaclePrototype : ScriptableObject
{
    public ObstacleType obstacle;
    public GameObject prefab;
    public int value;
}