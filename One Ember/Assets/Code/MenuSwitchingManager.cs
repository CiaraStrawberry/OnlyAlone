using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitchingManager : MonoBehaviour
{

    public RectTransform mainMenu;

    public RectTransform credits;

    public RectTransform instructions;

    public void Start()
    {
        moveToMainMenu();
    }
    public void openCredits ()
    {
        disableAll();
        credits.gameObject.SetActive(true);
    }

    public void moveToMainMenu ()
    {
        disableAll();
        mainMenu.gameObject.SetActive(true);
    }

    public void moveToinstructions ()
    {
        disableAll();
        instructions.gameObject.SetActive(true);
    }

    public void disableAll ()
    {
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
    }
}
