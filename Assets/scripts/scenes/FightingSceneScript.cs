using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSceneScript : MonoBehaviour {
    bool pausePrefInstantiated = false;
    Color skyColor;
    float redDifficultyChange;
    float greenDifficultyChange;
    float blueDifficultyChange;
    Color changingColor_tmp;

    void Awake() {
        ObjectsPool.Initialize();
    }
    void Start() {
        TEventManager<int>.AddListener(EventEnum.UnpauseEvent, EnablePausePref);
        TEventManager<int>.AddListener(EventEnum.IncreaseDifficultyEvent, ChangeBackgroundColor);
        skyColor = Camera.main.GetComponent<Camera>().backgroundColor;
        redDifficultyChange = (1 - skyColor.r) / 10;
        greenDifficultyChange = skyColor.g / 10;
        blueDifficultyChange = skyColor.b / 10;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePrefInstantiated) {
            pausePrefInstantiated = true;
            Instantiate(Resources.Load<GameObject>(@"prefabs\PausePref"));
        }
        if (Camera.main.GetComponent<Camera>().backgroundColor.r < skyColor.r && skyColor.r < 1) {
            changingColor_tmp.r = Camera.main.GetComponent<Camera>().backgroundColor.r + redDifficultyChange / 500;
            changingColor_tmp.g = Camera.main.GetComponent<Camera>().backgroundColor.g - greenDifficultyChange / 500;
            changingColor_tmp.b = Camera.main.GetComponent<Camera>().backgroundColor.b - blueDifficultyChange / 500;
            Camera.main.GetComponent<Camera>().backgroundColor = changingColor_tmp;
        }
    }

    void EnablePausePref(int noValue) {
        pausePrefInstantiated = false;
    }
    void ChangeBackgroundColor(int noValue) {
        if (skyColor.r < 1) {
            skyColor.r += Mathf.Min(redDifficultyChange, 1 - skyColor.r);
        }
        if (skyColor.g > 1) {
            skyColor.g -= Mathf.Min(greenDifficultyChange, skyColor.g);
        }
        if (skyColor.b > 1) {
            skyColor.b -= Mathf.Min(blueDifficultyChange, skyColor.b);
        }
    }
}
