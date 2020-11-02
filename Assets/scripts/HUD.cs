using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : TEventInvoker<int> {
    [SerializeField] Text scoreText;
    float curTimer;
    bool gameRuns;
    Timer difficultyIncreaseTimer;
    float changeDifficultyTime = ConfigurationUtils.DifficultyChangeTime;

    void Start() {
        difficultyIncreaseTimer = gameObject.AddComponent<Timer>();
        difficultyIncreaseTimer.Duration = changeDifficultyTime;
        difficultyIncreaseTimer.Run();
        difficultyIncreaseTimer.AddTimerFinishedListener(IncreaseDifficulty);
        TEventManager<int>.AddListener(EventEnum.ShipDestroyedEvent, StopGameTimer);
        events.Add(EventEnum.IncreaseDifficultyEvent, new IntEvent());
        TEventManager<int>.AddInvoker(EventEnum.IncreaseDifficultyEvent, this);
        events.Add(EventEnum.GameOver, new IntEvent());
        TEventManager<int>.AddInvoker(EventEnum.GameOver, this);

        curTimer = 0;
        gameRuns = true;
        scoreText.text = "Timer: " + curTimer.ToString();
    }

    void Update() {
        if (gameRuns) {
            curTimer += Time.deltaTime;
        }
        scoreText.text = "Timer: " + ((int)curTimer).ToString();
    }

    void StopGameTimer(int noValue) {
        gameRuns = false;
        difficultyIncreaseTimer.Stop();
        Instantiate(Resources.Load<GameObject>(@"prefabs\GameOverPref"));
        events[EventEnum.GameOver].Invoke((int)curTimer);
    }
    void IncreaseDifficulty() {
        events[EventEnum.IncreaseDifficultyEvent].Invoke(0);
        difficultyIncreaseTimer.Run();
    }
}
