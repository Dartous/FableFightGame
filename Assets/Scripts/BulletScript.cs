using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    #region Variables
    [Header("Can adjust these 2")]
    private float destroyAfter;
    private int attackForce;
    private bool applyForce;

    [Header("Do NOT touch these")]
    [SerializeField]
    private LayerMask CastMask;
    private Vector3 oldPos;
    [HideInInspector]
    public int damage = 1;
    private bool hasDamaged = false;
    public BulletSO bulletType;
    #endregion

    void Start()
    {
        //assign things depending on the bullet type
        destroyAfter = bulletType.destroyAfter;
        attackForce = bulletType.attackForce;
        applyForce = bulletType.applyForce;

        StartCoroutine(destroyTimer());
        oldPos = transform.position;
    }

    private IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(destroyAfter);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            UnitScript us = collision.transform.GetComponent<UnitScript>();

            //check if it has already dealt damage to prevent hitting multiple stuff at once
            //check if the target is damagable
            if (!hasDamaged && us.damagable)
            {
                //deal damage
                us.hp -= damage;

                //play sound
                FindObjectOfType<SoundScript>().Play(us.getHitSound);

                //set hasDamaged to true
                hasDamaged = true;
            }
            
            //apply force if the bullet applies force
            //check if the target is force affected
            if (applyForce && us.forceAffected)
            {
                //apply force to push the enemy back on impact
                collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * attackForce, ForceMode.Impulse);
            }

        }
        catch { }

        try
        {
            WizardHat wh = collision.transform.GetComponent<WizardHat>();

            //check if it has already dealt damage to prevent hitting multiple stuff at once
            //check if the target is damagable
            if (!hasDamaged && wh.damagable)
            {
                //deal damage
                wh.hp -= damage;

                //play sound
                FindObjectOfType<SoundScript>().Play(wh.getHitSound);

                //set hasDamaged to true
                hasDamaged = true;
            }

            //apply force if the bullet applies force
            //check if the target is force affected
            if (applyForce && wh.forceAffected)
            {
                //apply force to push the enemy back on impact
                collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * attackForce, ForceMode.Impulse);
            }

        }
        catch { }

        //destroy bullet
        Destroy(this.gameObject);
    }
}
