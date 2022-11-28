using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIContoller : MonoBehaviour
{
    public GameObject Menu;
    public GameObject HowToPlayImage;

    private Canvas canvas;

    private void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
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
