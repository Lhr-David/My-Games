using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using com;

[CustomEditor(typeof(MapConfig))]
public class MapConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapConfig t = target as MapConfig;
        if (GUILayout.Button("Import lv1 from xls"))
        {
            Import("lv1");
            OverrideLevel(t.level1ToOverride);
        }
        if (GUILayout.Button("Import lv2 from xls"))
        {
            Import("lv2");
            OverrideLevel(t.level2ToOverride);
        }
        if (GUILayout.Button("Import lv3 from xls"))
        {
            Import("lv3");
            OverrideLevel(t.level3ToOverride);
        }
    }

    void AddItem(ref int interval, ref List<ObstacleData> obstacles, ref MapConfig t, Place p, string obsString)
    {
        var item = new ObstacleData();
        item.distanceUnit = interval;
        item.obstacle = t.GetObstacleTypeBySymbol(obsString);
        item.place = p;
        interval = 0;
        obstacles.Add(item);
    }

    public void OverrideLevel(LevelPrototype level)
    {
        MapConfig t = target as MapConfig;
        level.obstacles = new List<ObstacleData>();
        int interval = 0;
        foreach (MapElement obs in t.mapXlsCache.elements)
        {
            interval++;
            if (obs.IsEmpty())
            {
                continue;
            }

            if (!string.IsNullOrEmpty(obs.Left))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Road_Left, obs.Left);
            }
            if (!string.IsNullOrEmpty(obs.LeftMid))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Road_LeftCenter, obs.LeftMid);
            }
            if (!string.IsNullOrEmpty(obs.SideLeft))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Side_Left, obs.SideLeft);
            }
            if (!string.IsNullOrEmpty(obs.Right))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Road_Right, obs.Right);
            }
            if (!string.IsNullOrEmpty(obs.RightMid))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Road_RightCenter, obs.RightMid);
            }
            if (!string.IsNullOrEmpty(obs.SideRight))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Side_Right, obs.SideRight);
            }
            if (!string.IsNullOrEmpty(obs.Mid))
            {
                AddItem(ref interval, ref level.obstacles, ref t, Place.Road_Center, obs.Mid);
            }
        }
        var n = target.name;
        var a = level.LevelLengthUnit;
        level.LevelLengthUnit = 1;
        level.LevelLengthUnit = a;
    }

    public void Import(string sheetName)
    {
        MapConfig t = target as MapConfig;
        SerializedObject so = new SerializedObject(t.mapXlsCache);
        so.Update();
        string filePath = XLSDatabase.FindSpreadSheetByName("LD");
        Debug.Log(filePath);
        Debug.Log(so);
        var property = so.FindProperty("elements");
        if (filePath != null)
        {
            if (property != null)
            {
                XLSDatabase.ImportList(filePath, sheetName, property);
                so.ApplyModifiedProperties();
                Debug.Log("Import done");
            }
            else
            {
                Debug.Log("Import error: property does not exist");
            }
        }
        else
        {
            Debug.Log("Import error: xls file does not exist");
        }
        so.ApplyModifiedProperties();
    }
}

