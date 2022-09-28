using UnityEngine;
using System.Collections;

public class LevelPicker : MonoBehaviour
{
    public static int currentLevelIndex = 0;

    public static void OnFail()
    {
        currentLevelIndex += 1;
    }

    public static void OnWin()
    {
        //currentLevelIndex += 1;
    }
}
