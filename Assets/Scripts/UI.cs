using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GridPlacing Build;

    //[SerializeField] AnimationCurve Curve;
    //public GameObject Menu_Button;
    public float UIHidden_YPos;
    public float UIShown_YPos;
    private float t = 0;
    private float PassedTime;
    private float LengthofTime;
    private bool is_MenuAnimComplete;
    public Image hatImg;
    public Image SkullImg;
    public Image MushroomImg;
    public Image CubeImg;
    private Image focusImg;
    // Start is called before the first frame update

    public void Awake()
    {
        UIHidden_YPos = -50;
        UIShown_YPos = 90;
        LengthofTime = 0.2f;
        is_MenuAnimComplete = true;
    }

    public void Start()
    {
        focusImg = hatImg;
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
    public void Hat_TSelect()
    {
        FindObjectOfType<SoundScript>().Play("Click", 0.5f);
        Build.scrollval = 0;

        setFocusImg(hatImg);
    }
    public void Skull_TSelect()
    {
        FindObjectOfType<SoundScript>().Play("Click", 0.5f);
        Build.scrollval = 1;

        setFocusImg(SkullImg);
    }
    public void Mush_TSelect()
    {
        FindObjectOfType<SoundScript>().Play("Click", 0.5f);
        Build.scrollval = 3;

        setFocusImg(MushroomImg);
    }
    public void GC_TSelect()
    {
        FindObjectOfType<SoundScript>().Play("Click", 0.5f);
        Build.scrollval = 2;

        setFocusImg(CubeImg);
    }
    public void setFocusImg(Image img)
    {
        focusImg.rectTransform.sizeDelta = new Vector2(160, 30);
        focusImg = img;
        focusImg.rectTransform.sizeDelta = new Vector2(220, 50);
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

    public void setFocusImgSize (Image img)
    {
        focusImg.rectTransform.sizeDelta = new Vector2(160, 30);
        focusImg = img;
        focusImg.rectTransform.sizeDelta = new Vector2(200, 50);
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
            FindObjectOfType<SoundScript>().Play("Click", 0.5f);
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
