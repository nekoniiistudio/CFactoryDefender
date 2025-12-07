using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewScriptableObjectEvent", menuName = "RSUnityFramework/Events/ScriptableObjectEvent")]
public class ScriptableObjectEventBase : ScriptableObject
{
    [SerializeField]
    private UnityEvent onRaised = new UnityEvent();

    private readonly List<Action> runtimeListeners = new List<Action>();

    // Register a runtime listener (C# code)
    public void Register(Action listener)
    {
        if (listener == null) return;
        if (!runtimeListeners.Contains(listener))
            runtimeListeners.Add(listener);
    }

    // Unregister a runtime listener
    public void Unregister(Action listener)
    {
        if (listener == null) return;
        runtimeListeners.Remove(listener);
    }

    // Raise the event: invoke inspector UnityEvent first, then runtime listeners
    public void Raise()
    {
        onRaised?.Invoke();

        // Iterate backwards in case listeners unregister during invocation
        for (int i = runtimeListeners.Count - 1; i >= 0; i--)
        {
            try
            {
                runtimeListeners[i]?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }
        }
    }

    // Clear runtime listeners (useful for tests or reset)
    public void ClearRuntimeListeners() => runtimeListeners.Clear();
}