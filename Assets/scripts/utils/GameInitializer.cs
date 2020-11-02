using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {
    void Start() {
        TEventManager<int>.Initialize();
        ConfigurationUtils.Initialize();
    }
}
