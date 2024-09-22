using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuitOrPause : MonoBehaviour
{
    private static Action quitActions;
    public static void Add(Action action)
    {
        quitActions += action;
    }
    public static void Remove(Action action)
    {
        quitActions -= action;
    }
    public static void RemoveAll()
    {
        foreach (var @delegate in quitActions.GetInvocationList())
        {
            var quitAction = (Action) @delegate;
            quitActions -= quitAction;
        }
    }

    private static void InvokeQuitActions()
    {
        if (quitActions != null)
        {
            quitActions.Invoke();
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            InvokeQuitActions();
        }
    }
#endif

#if UNITY_EDITOR || UNITY_IOS
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            InvokeQuitActions();
        }
    }

    private void OnApplicationQuit()
    {
        InvokeQuitActions();
    }
#endif
}
