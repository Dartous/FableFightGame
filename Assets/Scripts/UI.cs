using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GridBuild Build;

    //[SerializeField] AnimationCurve Curve;
    //public GameObject Menu_Button;
    public float UIHidden_YPos;
    public float UIShown_YPos;
    private float t = 0;
    private float PassedTime;
    private float LengthofTime;
    private bool is_MenuAnimComplete;
    // Start is called before the first frame update

    public void Awake()
    {
        UIHidden_YPos = -50;
        UIShown_YPos = 90;
        LengthofTime = 0.2f;
        is_MenuAnimComplete = true;
    }

    public void MenuAppearTransition()
    {
        t = PassedTime / LengthofTime;
        float AH = Mathf.Lerp(UIHidden_YPos, UIShown_YPos, t);
        Vector3 newpos = new Vector3(gameObject.transform.position.x, AH, gameObject.transform.position.z);
        gameObject.transform.position = newpos;
        PassedTime += Time.deltaTime;
        
       //Vector3 Stuck = gameObject.transform.position.Lerp(ShowUIPos, HideUIPos, 1);
    }


    public void MenuDissapearTransition()
    {

    }
    public void GC_TSelect()
    {
        Build.scrollval = 1 - 1;
    }

    public void Mush_TSelect()
    {
        Build.scrollval = 2 - 1;
    }
    public void Skull_TSelect()
    {
        Build.scrollval = 3 - 1;
    }
    IEnumerator PopupLoop()
    {
        is_MenuAnimComplete = false;
        while (PassedTime < LengthofTime)
        {
            
            MenuAppearTransition();
        }
        yield return null;
        print("done");
    }

    public void PlaceholderModel2()
    {
        //print("hi");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (PassedTime < LengthofTime && is_MenuAnimComplete)
            {
                StartCoroutine("PopupLoop");
            }
            else
            {
                print("Menu is still in process");
            }
        }

    }
}
