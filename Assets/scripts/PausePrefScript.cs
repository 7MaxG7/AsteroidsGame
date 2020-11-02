using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePrefScript : TEventInvoker<int> {
    void Start() {
        events.Add(EventEnum.UnpauseEvent, new IntEvent());
        TEventManager<int>.AddInvoker(EventEnum.UnpauseEvent, this);
        Time.timeScale = 0;
    }

    public void OnClickContinueButton() {
        events[EventEnum.UnpauseEvent].Invoke(0);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    public void OnClickMainMenuButton() {
        OnClickContinueButton();
        ObjectsPool.ReturnAllObjectsToPool();
        MenuManager.GoTo(MenuEnum.MainMenu);
    }
}
