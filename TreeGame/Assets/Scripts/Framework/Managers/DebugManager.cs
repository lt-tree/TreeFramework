
#define USE_DEBUG

using UnityEngine;
using System;
using System.Collections.Generic;



#if RELEASE_MODE

// release模式重载DEBUG，使其失效
public static class Debug
{
    public static void Log(object message) { }
    public static void Log(object message, object context) { }
    public static void LogError(object message) { }
    public static void LogError(object message, object context) { }
    public static void LogException(Exception exception) { }
    public static void LogException(Exception exception, object context) { }
    public static void LogWarning(object message) { }
    public static void LogWarning(object message, object context) { }
    public static void DrawLine(Vector3 start, Vector3 end) { }
    public static void DrawLine(Vector3 start, Vector3 end, Color color) { }
}

#endif

public static class Debugger
{
    public static void Log(object message)
    {
    #if RELEASE_MODE

    #else
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            DebugManager.Log(message.ToString());
        else
            UnityEngine.Debug.Log(message.ToString());
    #endif
    }

    public static void LogError(object message)
    {
    #if RELEASE_MODE

    #else
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            DebugManager.Log(message.ToString());
        else
            UnityEngine.Debug.LogError(message.ToString());
    #endif
    }

    public static void LogWarning(object message)
    {
    #if RELEASE_MODE

    #else
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            DebugManager.Log(message.ToString());
        else
            UnityEngine.Debug.LogWarning(message.ToString());
    #endif
    }
}



public class DebugManager : MonoBehaviour
{
    static public bool DebugInfo = false;
    static public Int32 DebugCount = 0;

    static public List<string> errorInfoList = new List<string>();

    private void Start()
    {
    }

    private void Update()
    {
    }

    public static void Switch()
    {
        DebugInfo = !DebugInfo;
        if(DebugInfo)
        {
            ++DebugCount;
            errorInfoList.Clear();
            errorInfoList.Add(DebugCount.ToString());
        }
    }

    public static void Log(string str)
    {
        errorInfoList.Add(str);
    }

    public Rect errorInfoWindowRect = new Rect(80, 20, 800, 2000);
    private void OnGUI()
    {
        GUILayout.Space(40);
        if(GUILayout.Button("ShowError"))
        {
            DebugInfo = !DebugInfo;
        }
        else if (GUILayout.Button("Clear"))
        {
            errorInfoList.Clear();
        }

        if(DebugInfo)
        {
            errorInfoWindowRect = GUILayout.Window(1, errorInfoWindowRect, DebugErrorWindow, "Debug Error Window");
        }
    }

    private Vector2 errorInfoPos = new Vector2(0, 0);
    void DebugErrorWindow(int windowID)
    {
        errorInfoPos = GUILayout.BeginScrollView(errorInfoPos, false, true, GUILayout.Width(800), GUILayout.Height(600));

        GUILayout.Space(30);
        GUILayout.BeginVertical();

        foreach(string str in errorInfoList)
        {
            GUILayout.Label(str, GUILayout.Width(800));
        }

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
}













