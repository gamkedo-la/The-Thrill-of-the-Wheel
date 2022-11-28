using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuUIContoller : MonoBehaviour
{
    public GameObject Menu;
    public GameObject HowToPlayImage;

    private Canvas canvas;

    private DriveInputs _driveInputs;
    private InputAction _menuAction;

    private void Awake()
    {
        _driveInputs = new DriveInputs();
        _menuAction = _driveInputs.Player.Menu;
    }

    private void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (_menuAction.triggered) ShowOrHideMenu();
    }

    private void OnEnable()
    {
        _menuAction.Enable();
    }

    private void OnDisable()
    {
        _menuAction.Disable();
    }

    private void ShowOrHideMenu()
    {
        ShowOrHideMenu(!canvas.isActiveAndEnabled);
    }

    private void ShowOrHideMenu(bool isToShow)
    {
        if (isToShow)
        {
            SetMenuToDefault();
        }

        canvas.enabled = isToShow;
    }

    private void SetMenuToDefault()
    {
        Menu.SetActive(true);
        HowToPlayImage.SetActive(false);
    }

    #region Button Clicks
    public void ShowCredits()
    {
        //TO DO: Is not set up yet.
    }

    public void ShowHowToPlayImage()
    {
        Menu.SetActive(false);
        HowToPlayImage.SetActive(true);
    }
    #endregion
}
