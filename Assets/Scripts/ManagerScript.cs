using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ManagerScript : MonoBehaviour
{
    public bool paused;
    public GameObject griddy;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            Time.timeScale = 0;
            paused = !paused; // flips the bool value.
            griddy.gameObject.GetComponent<GridBuild>().enabled = false;
            canvas.gameObject.SetActive(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            Time.timeScale = 1;
            paused = !paused;
            griddy.gameObject.GetComponent<GridBuild>().enabled = true;
            canvas.gameObject.SetActive(false);
            return;
        }
    }
}
