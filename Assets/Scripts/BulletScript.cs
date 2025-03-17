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
        //add bool to check if has dealt damage
        try
        {
            if (!hasDamaged)
            {
                //deal damage
                collision.transform.GetComponent<UnitScript>().hp -= damage; collision.transform.GetComponent<UnitScript>().hp -= damage;
                hasDamaged = true;
            }
            
            if (applyForce)
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
