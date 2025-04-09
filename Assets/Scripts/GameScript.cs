using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    #region Initialize Variables
    public int knowledge = 0;
    public int maxKnowledge = 20;
    public TMP_Text knowledgeBank;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("updateKnowledge", 0, 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void updateKnowledge()
    {
        knowledgeBank.text = knowledge.ToString();
    }
}
