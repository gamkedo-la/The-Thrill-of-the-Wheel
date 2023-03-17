using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;
    bool isLose;
    // Start is called before the first frame update
    void Start()
    {
        string endState = PlayerPrefs.GetString("EndState");
        Debug.Log(endState);
        if(endState == "win") {
            winText.SetActive(true);
        } else {
            isLose = true;
            loseText.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isLose) {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene("CarSelection"));
        } else {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => Application.Quit());
        }
    }
}
