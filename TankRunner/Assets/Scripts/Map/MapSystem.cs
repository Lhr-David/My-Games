using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public static MapSystem instance { get; private set; }

    public MapConfig config;

    public Transform obsParent;
    public int distanceUnitAfterLastElement;

    LevelPrototype _currentLevel;

    float _levelEndDistance;
    int _obstacleIndex;
    float _restDistUnit;
    ObstacleData _nextObs;

    public bool testWin;

    private void Update()
    {
        if (testWin)
        {
            testWin = false;
            GameSystem.instance.Win();

        }
    }
    private void Awake()
    {
        instance = this;
    }

    public void StartLevel(int lv)
    {
        StartLevel(config.levels[lv]);
    }

    public void StartLevel(LevelPrototype level)
    {
        _obstacleIndex = 0;
        _restDistUnit = 0;
        _levelEndDistance = -1;

        _currentLevel = level;
        GameHudBehaviour.instance.SetLevelTitle(_currentLevel.levelName);
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

    public void TickMapProcess()
    {
        _restDistUnit -= 1;
    }

    public void CheckMapFinish(float playerZ)
    {
        if (_levelEndDistance < 0)
            return;

        if (playerZ > _levelEndDistance)
            GameSystem.instance.Win();
    }

    public void ShowObstacles(int playerZ)
    {
        if (_obstacleIndex >= _currentLevel.obstacles.Count)
        {
            if (_levelEndDistance < 0)
            {
                _levelEndDistance = playerZ + distanceUnitAfterLastElement * config.distancePerUnit + config.obstacleOffset;
            }
            return;
        }

        if (_nextObs == null)
        {
            _nextObs = _currentLevel.obstacles[_obstacleIndex];
            _restDistUnit = _nextObs.distanceUnit * config.distancePerUnit;
        }

        if (_restDistUnit <= 0)
        {
            _obstacleIndex++;
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
