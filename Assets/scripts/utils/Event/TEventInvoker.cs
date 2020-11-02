using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TEventInvoker<T> : MonoBehaviour {
    protected Dictionary<EventEnum, UnityEvent<T>> events = new Dictionary<EventEnum, UnityEvent<T>>();

    public void AddListener(EventEnum eventName, UnityAction<T> listener_foo) {
        if (events.ContainsKey(eventName)) {
            events[eventName].AddListener(listener_foo);
        }
    }
}
