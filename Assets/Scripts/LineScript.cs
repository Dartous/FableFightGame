using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseLineScript : MonoBehaviour
{
    //get the line
    public int lineHP = 2;

    [Header("For TESTING only")]
    public int currentLineHP;

    // Start is called before the first frame update
    void Start()
    {
        currentLineHP = lineHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void checkLineHP()
    {
        if (currentLineHP <= 0)
        {
            //game over
            print("Game Over");
            SceneManager.LoadScene(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layerIndex = collision.gameObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);

        if (layerName == "Enemy")
        {
            currentLineHP--;
            checkLineHP();
        }
    }
}
