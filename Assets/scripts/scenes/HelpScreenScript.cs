using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreenScript : MonoBehaviour {
    public void OnClickMainMenuButton() {
        MenuManager.GoTo(MenuEnum.MainMenu);
    }
}
