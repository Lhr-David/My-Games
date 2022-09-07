using UnityEngine;
using System.Collections.Generic;

[System.Serializable, CreateAssetMenu]
public class MapXlsCache : ScriptableObject
{
    public List<MapElement> elements;

}

[System.Serializable]
public class MapElement
{
    public int row;
    public string SideLeft;
    public string Left;
    public string LeftMid;
    public string Mid;
    public string RightMid;
    public string Right;
    public string SideRight;

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(SideLeft) &&
            string.IsNullOrEmpty(Left) &&
            string.IsNullOrEmpty(LeftMid) &&
            string.IsNullOrEmpty(Mid) &&
            string.IsNullOrEmpty(RightMid) &&
            string.IsNullOrEmpty(Right) &&
            string.IsNullOrEmpty(SideRight);
    }
}