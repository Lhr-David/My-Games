using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroundSystem : MonoBehaviour
{
    public static GroundSystem instance;

    public Transform player;

    public GroundBehaviour prefab;
    public Transform groundParent;

    private Dictionary<Vector2Int, GroundBehaviour> _grounds;

    public int sizeDisplayWidth;
    public int sizeDisplayForward;
    public int sizeDisplayBackward;

    public float tickTime;

    float _tickTimestamp;
    int _gen;

    private void Awake()
    {
        _tickTimestamp = -1;
        instance = this;
        _grounds = new Dictionary<Vector2Int, GroundBehaviour>();
    }

    public void StartTimer()
    {
        _tickTimestamp = Time.time;
        _gen = 0;
    }

    public void StopTimer()
    {
        _tickTimestamp = -1;
    }

    private void Update()
    {
        if (_tickTimestamp < 0)
            return;

        if (Time.time > _tickTimestamp)
        {
            _tickTimestamp += tickTime;
            Curling();
        }
    }

    void Curling()
    {
        //Debug.Log(_gen);
        _gen++;
        int x = 0;
        int z = Mathf.CeilToInt(player.position.z);

        for (int i=0;i<7;i++)
        {
            MapSystem.instance.ShowObstacles(z);
        }
        MapSystem.instance.TickMapProcess();

        for (int i = x - sizeDisplayWidth; i <= x + sizeDisplayWidth; i++)
        {
            for (int j = z - sizeDisplayBackward; j <= z + sizeDisplayForward; j++)
            {
                Vector2Int pPos = new Vector2Int(i, j);
                if (_grounds.ContainsKey(pPos) && _grounds[pPos] != null)
                {
                    _grounds[pPos].gen = _gen;
                }
                else
                {
                    var tile = Instantiate<GroundBehaviour>(prefab, groundParent);
                    tile.gameObject.SetActive(true);
                    tile.gen = _gen;
                    tile.SetPos(i, j);
                    if (_grounds.ContainsKey(pPos))
                        _grounds[pPos] = tile;
                    else
                        _grounds.Add(pPos, tile);
                }
            }
        }

        foreach (var s in _grounds.Where(
             kv => kv.Value == null).ToList())
        {
            _grounds.Remove(s.Key);
        }

        foreach (var tile in _grounds)
        {
            if (tile.Value == null || tile.Value.gameObject == null)
                continue;

            if (tile.Value.gen != _gen)
            {
                Destroy(tile.Value.gameObject);
            }
            else
            {
                tile.Value.SyncPos();
            }
        }

        MapSystem.instance.CheckMapFinish(player.position.z);
    }
}
