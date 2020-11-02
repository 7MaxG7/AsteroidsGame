using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager {
    public static void GoTo(MenuEnum menu_foo) {
        switch (menu_foo) {
            case MenuEnum.MainMenu:
                SceneManager.LoadScene("01_MainMenu");
                break;
            case MenuEnum.StartGame:
                SceneManager.LoadScene("03_FightingScene");
                break;
            case MenuEnum.HelpScreen:
                SceneManager.LoadScene("02_HelpScreen");
                break;
            case MenuEnum.QuitGame:
                Application.Quit();
                break;
        }
        AudioManager.Play(AudioClipName.MenuButton);
    }
}
