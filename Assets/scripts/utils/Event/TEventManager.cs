using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TEventManager<T> {
    static Dictionary<EventEnum, List<TEventInvoker<T>>> invokers = new Dictionary<EventEnum, List<TEventInvoker<T>>>();
    static Dictionary<EventEnum, List<UnityAction<T>>> listeners = new Dictionary<EventEnum, List<UnityAction<T>>>();
    public static void Initialize() {
        foreach(EventEnum eventEnum in Enum.GetValues(typeof(EventEnum))) {
            if (invokers.ContainsKey(eventEnum)) {
                invokers[eventEnum].Clear();
                listeners[eventEnum].Clear();
            } else {
                invokers.Add(eventEnum, new List<TEventInvoker<T>>());
                listeners.Add(eventEnum, new List<UnityAction<T>>());
            }
        }
    }

    public static void AddInvoker(EventEnum eventName, TEventInvoker<T> invoker_foo) {
        if (!invokers[eventName].Contains(invoker_foo)) {
            invokers[eventName].Add(invoker_foo);
            foreach (UnityAction<T> listener in listeners[eventName]) {
                invoker_foo.AddListener(eventName, listener);
            }
        }
    }
    public static void AddListener(EventEnum eventName, UnityAction<T> listener_foo) {
        if (!listeners[eventName].Contains(listener_foo)) {
            listeners[eventName].Add(listener_foo);
            foreach (TEventInvoker<T> invoker in invokers[eventName]) {
                invoker.AddListener(eventName, listener_foo);
            }
        }
    }
    public static void RemoveInvoker(EventEnum eventName, TEventInvoker<T> invoker_foo) {
        invokers[eventName].Remove(invoker_foo);
    }
}
