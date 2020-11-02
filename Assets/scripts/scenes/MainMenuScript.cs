using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {
    public void OnClickPlayButton() {
        MenuManager.GoTo(MenuEnum.StartGame);
    }
    public void OnClickHelpButton() {
        MenuManager.GoTo(MenuEnum.HelpScreen);
    }
    public void OnClickQuitButton() {
        MenuManager.GoTo(MenuEnum.QuitGame);
    }
}
