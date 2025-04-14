using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Pause_Button : MonoBehaviour
{ //starting to see the use of structs now..
    public ManagerScript ManagerScript;
    public GameObject griddy;
    public Canvas canvas;

    public GameObject mainPause;
    public GameObject PauseBack;
    public GameObject Settings;

    private void Start()
    {
        mainPause.SetActive(false);
        Settings.SetActive(false);
        PauseBack.SetActive(false);
        Time.timeScale = 1.0f;
        ManagerScript.paused = false;

    }

    public void Pause()
    {
        if (!ManagerScript.paused)
        {
            print("I");
            Time.timeScale = 0;
            ManagerScript.paused = !ManagerScript.paused; // flips the bool value.
            griddy.gameObject.GetComponent<GridPlacing>().enabled = false;
            mainPause.SetActive(true);
            PauseBack.SetActive(true);
            return;
        }
    }
    public void PauseInverse()
    {
        if (ManagerScript.paused)
        {
            Time.timeScale = 1;
            ManagerScript.paused = !ManagerScript.paused;
            griddy.gameObject.GetComponent<GridPlacing>().enabled = true;
            mainPause.SetActive(false);
            PauseBack.SetActive(false);
            return;
        }
    }

    public void Setttings()
    {
        mainPause.SetActive(false);
        Settings.SetActive(true);
    }

    public void Back()
    {
        mainPause.SetActive(true);
        Settings.SetActive(false);
    }
}
