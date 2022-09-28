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

    public MapXlsCache mapXlsCache;

    public List<XlsSymbolMapping> ObstacleBySymbol;

    public LevelPrototype level1ToOverride;
    public LevelPrototype level2ToOverride;
    public LevelPrototype level3ToOverride;

    [System.Serializable]
    public struct XlsSymbolMapping
    {
        public string s;
        public ObstacleType obs;
    }

    public ObstacleType GetObstacleTypeBySymbol(string s)
    {
        foreach (var o in ObstacleBySymbol)
        {
            if (s == o.s)
            {
                return o.obs;
            }
        }

        return ObstacleType.Coin;
    }
}
