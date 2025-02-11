using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    #region Variables
    [Header("Can adjust these 2")]
    public bool isDestroyedOnHit;
    public int destroyAfter = 6;

    [Header("Do NOT touch these")]
    [SerializeField]
    private LayerMask CastMask;
    private Vector3 oldPos;
    [HideInInspector]
    public int damage = 1;
    #endregion

    void Start()
    {
        isDestroyedOnHit = true;
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
            //deal damage
            try
            {
                collision.transform.GetComponent<EnemyScript>().hp -= damage;
            }
            catch 
            {
                collision.transform.GetComponent<FriendlyScript>().hp -= damage;
            }
        }
        catch { }

        //check if its destroyed on hit
        if (isDestroyedOnHit)
        {
            //destroy bullet
            Destroy(this.gameObject);
        }
    }
}
