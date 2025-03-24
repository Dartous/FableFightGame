using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Pause_Button : MonoBehaviour
{ //starting to see the use of structs now..
    public ManagerScript ManagerScript;
    public GameObject griddy;
    public Canvas canvas;
    public void Pause()
    {
        if (!ManagerScript.paused)
        {
            print("I");
            Time.timeScale = 0;
            ManagerScript.paused = !ManagerScript.paused; // flips the bool value.
            griddy.gameObject.GetComponent<GridBuild>().enabled = false;
            canvas.gameObject.SetActive(true);
            return;
        }
    }
    public void PauseInverse()
    {
        if (ManagerScript.paused)
        {
            Time.timeScale = 1;
            ManagerScript.paused = !ManagerScript.paused;
            griddy.gameObject.GetComponent<GridBuild>().enabled = true;
            canvas.gameObject.SetActive(false);
            return;
        }
    }
}
