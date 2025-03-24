using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    public int scrollval = 0 - 1;
    public List<GameObject> Asset = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int ScrollRight()
    {
        int max = Asset.Count;
        if (max != scrollval)
        {
            scrollval = scrollval + 1;
            return scrollval;
        }
        else
        {
            print("This is the maximum asset it can go");
            return 0;
        }
    }

    public int ScrollLeft()
    {
        int max = Asset.Count;
        if (-1 != scrollval)
        {
            scrollval = scrollval - 1;
            return scrollval;
        }
        else
        {
            print("This is the minimum asset it can go");
            return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Q)))
        {
            ScrollLeft();

        }
        if (Input.GetKeyDown((KeyCode.E)))
        {
            ScrollRight();
        }
    }
}
