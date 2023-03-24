using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{
    public string sceneNameToLoad = "CarSelection";

    void Update()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene(sceneNameToLoad));
    }
}
