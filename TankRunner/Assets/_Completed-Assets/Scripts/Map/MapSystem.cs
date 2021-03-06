using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public static MapSystem instance { get; private set; }

    public MapConfig config;

    public Transform obsParent;

    LevelPrototype _currentLevel;

    int _obstacleIndex;
    float _restDistUnit;
    ObstacleData _nextObs = null;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartLevel(config.levels[0]);
    }

    void StartLevel(LevelPrototype level)
    {
        _obstacleIndex = 0;
        _restDistUnit = 0;
        _nextObs = null;

        _currentLevel = level;
    }

    public static float GetXOffset(Place place)
    {
        switch (place)
        {
            case Place.Road_Center:
                return 0;

            case Place.Road_Left:
                return 0 - instance.config.xOffsetDelta * 2;

            case Place.Road_Right:
                return 0 + instance.config.xOffsetDelta * 2;

            case Place.Road_LeftCenter:
                return 0 - instance.config.xOffsetDelta * 1;

            case Place.Road_RightCenter:
                return 0 + instance.config.xOffsetDelta * 1;

            case Place.Side_Right:
                return 0 + instance.config.xOffsetDelta * 5;

            case Place.Side_Left:
                return 0 - instance.config.xOffsetDelta * 5;
        }

        return 0;
    }

    public void ShowObstacles(int playerZ)
    {
        if (_obstacleIndex >= _currentLevel.obstacles.Count)
            return;

        if (_nextObs == null)
        {
            _nextObs = _currentLevel.obstacles[_obstacleIndex];
            _obstacleIndex++;
            _restDistUnit = _nextObs.distanceUnit*config.distancePerUnit;
        }

        _restDistUnit -= 1;
        if (_restDistUnit < 0)
        {
            var go = Instantiate(GetPrefab(_nextObs.obstacle), obsParent);
            go.transform.position = GetPosition(_nextObs.place, playerZ);
            _nextObs = null;
        }

        // Debug.Log("ShowObstacles " + playerZ);
    }

    GameObject GetPrefab(ObstacleType type)
    {
        var defs = config.obstacles;
        foreach (var d in defs)
        {
            if (d.obstacle == type)
                return d.prefab;
        }

        return null;
    }

    Vector3 GetPosition(Place place, int currentPlayerZ)
    {
        return new Vector3(GetXOffset(place), -0.5f, currentPlayerZ + config.obstacleOffset);
    }
}
