using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPref : MonoBehaviour {
    [SerializeField] Text timeScore;
    void Awake() {
        TEventManager<int>.AddListener(EventEnum.GameOver, ShowTimeScore);
    }

    public void OnClickRestartButton() {
        ObjectsPool.ReturnAllObjectsToPool();
        MenuManager.GoTo(MenuEnum.StartGame);
    }
    public void OnClickMainMenuButton() {
        ObjectsPool.ReturnAllObjectsToPool();
        MenuManager.GoTo(MenuEnum.MainMenu);
    }
    void ShowTimeScore(int timeScore) {
        this.timeScore.text = "Survived: " + timeScore + " sec";
    }
}
