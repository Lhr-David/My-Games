using UnityEngine;
using System;

[Serializable]
public class ObstacleData
{
    public Place place;
    public ObstacleType obstacle;
    public int distanceUnit;
}

public enum Place
{
    Road_Center = 10,
    Road_RightCenter = 20,
    Road_LeftCenter = 30,
    Road_Right = 40,
    Road_Left = 50,
    Side_Left = 60,
    Side_Right = 70,
}

public enum ObstacleType
{
    Tree = 10,
    Coin = 20,
    Turret = 30,
    House = 40,

    Pickup_magnet=101,
    Pickup_invinsible = 102,
    Pickup_heal = 103,
}