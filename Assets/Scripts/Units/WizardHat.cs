using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class WizardHat : MonoBehaviour
{
    #region Initiate Variables
    public int cost = 20;
    public int hp = 1;
    public int knowledge = 0;
    public int maxKnowledge = 10;
    public float knowledgeGenerationStartDelayInSeconds = 1;
    public int knowledgeGenerationPerTick = 2;
    public float knowledgeGenerationTickRateInSeconds = 1;
    public bool damagable = true;
    public bool forceAffected = false;
    public bool readyToBeCollected = false;
    private bool isDead = false;

    [Header("Make the name exactly as in the SoundScript")]
    public string getHitSound = "WizardHatGetHit";
    public string readyToCollectSound = "WizardHatReadyToCollect";

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody rb;
    [Header("Attack These")]
    public GameScript gs;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //call generate knowledge function with a specified start delay and tick rate
        InvokeRepeating("GenerateKnowledge", knowledgeGenerationStartDelayInSeconds, knowledgeGenerationTickRateInSeconds);
    }

    // Update is called once per frame
    private void Update()
    {
        //if its not dead
        if (!isDead)
        {
            //if 0 hp - destroy
            CheckHP();

            //Idle animation/stuff
            Idle();
        }
            
    }
    private void Idle()
    {
        //play idle animation
    }

    //if 0 hp destroy
    private void CheckHP()
    {
        if (hp <= 0)
        {
            isDead = true;
            Destroy(this.gameObject);
        }
    }

    //generate knowledge
    private void GenerateKnowledge()
    {
        if (knowledge < maxKnowledge)
        {
            knowledge += knowledgeGenerationPerTick;
        }
        //else set the knowledge to be ready to be collected
        else
        {
            if (!readyToBeCollected)
            {
                //play sound
                FindObjectOfType<SoundScript>().Play(readyToCollectSound);
            }

            readyToBeCollected = true;
        }
    }

    //on mouse down collect knowledge
    private void OnMouseDown()
    {
        //check if its ready to be collected
        //check if the knowledge bank is full
        bool isFull = gs.knowledge >= gs.maxKnowledge;
        if (readyToBeCollected && !isFull)
        {
            //add collected knowledge to the bank
            gs.knowledge += knowledge;

            //reset current knowledge on the unit
            knowledge = 0;

            //set ready to be collected to false
            readyToBeCollected = false;
        }
        else if (!readyToBeCollected)
        {
            print("Knowledge is NOT ready to be collected");
        }
        else if (isFull)
        {
            print("Knowledge storage is FULL");
        }
    }
}
