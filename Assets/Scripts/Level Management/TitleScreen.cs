using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    public string sceneNameToLoad = "CarSelection";
    public TMP_Text loadingText;

    void Update()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl => ButtonResponse());
    }

    void ButtonResponse() {
        if(loadingText != null) {
            loadingText.text = "Loading...";
        }
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
