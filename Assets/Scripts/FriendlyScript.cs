using UnityEngine;
using UnityEngine.UIElements;

public class FriendlyScript : MonoBehaviour
{
    #region Initiate Variables
    [Header("Attach these")]
    public Transform attackPoint;
    public UnitSO unitType;

    //get this from the UnitSO
    [HideInInspector]
    public int hp;
    private float attackForce;
    private int attackDamage;
    private float attackSpeed;
    private float attackRange;
    private bool damagable;
    private bool forceAffected;
    private GameObject bullet;

    //other private stuff
    private Animator animator;
    private bool canAttack = true;
    private bool playerInAttackRange = false;
    private bool isDead = false;
    
    //allow for 2 fighting states selection
    private FightingState fightingState;
    private enum FightingState
    {
        melee,
        range
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Assign this stuff depending on their enemyType
        hp = unitType.hp;
        attackForce = unitType.attackForce;
        attackDamage = unitType.attackDamage;
        attackSpeed = unitType.attackSpeed;
        attackRange = unitType.attackRange;
        damagable = unitType.damagable;
        forceAffected = unitType.forceAffected;
        bullet = unitType.bullet;

        //Assign other stuff
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //if 0 hp - destroy
        CheckHP();
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

    //reset canAttack
    private void ResetAttack()
    {
        canAttack = true;
    }


    private void Fire()
    {
        Rigidbody rb = Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
        rb.GetComponent<BulletScript>().damage = attackDamage;

        canAttack = false;
        Invoke(nameof(ResetAttack), attackSpeed);
    }
}
