using UnityEngine;
using System;

[Serializable]
public class ObstaclePrototype
{
    public Place place;
    public ObstacleType obstacle;
    public int distanceUnit;
}

public enum Place
{
    RoadCenter,
    RoadRight,
    RoadLeft,
    SideLeft,
    SideRight,
}

public enum ObstacleType
{
    Tree = 10,
    Coin = 20,
    Turret = 30,
    SideRight = 40,
}