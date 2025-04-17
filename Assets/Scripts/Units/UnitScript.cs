using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitScript : MonoBehaviour
{
    #region Initiate Variables
    [Header("Attach these")]
    public Transform attackPoint;
    public UnitSO unitType;

    //get this from the UnitSO
    [HideInInspector]
    public bool enemy;
    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public float moveSpeed;
    //[ReadOnly]
    [Header("Do not edit")]
    public int hp;
    [HideInInspector]
    public float attackForce;
    [HideInInspector]
    public int attackDamage;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float attackRange;
    [HideInInspector]
    public int attackLanes;
    [HideInInspector]
    public bool damagable;
    [HideInInspector]
    public bool forceAffected;
    [HideInInspector]
    public GameObject bullet;

    [Header("Sounds")]
    [HideInInspector]
    public string getHitSound;
    [HideInInspector]
    public string attackSound;
    [HideInInspector]
    public string gruntSound;

    //other stuff
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public LayerMask whatIsEnemy;
    [HideInInspector]
    public LayerMask whatIsTower;
    [HideInInspector]
    public Animator animator;
    [Header("For TESTING Only")]
    public bool canAttack = true;
    public bool hasAttacked = false;
    public bool targetInAttackRange = false;
    [HideInInspector]
    public bool isDead = false;

    //upgrades
    public bool upgradeReady = false;

    //allow for 3 states selection
    [HideInInspector]
    public State state;
    [HideInInspector]
    public enum State
    {
        idle,
        moving,
        attacking
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        //Assign this stuff depending on their unitType
        enemy = unitType.enemy;
        canMove = unitType.canMove;
        moveSpeed = unitType.moveSpeed;
        hp = unitType.hp;
        attackForce = unitType.attackForce;
        attackDamage = unitType.attackDamage;
        attackSpeed = unitType.attackSpeed;
        attackRange = unitType.attackRange;
        attackLanes = unitType.attackLanes;
        damagable = unitType.damagable;
        forceAffected = unitType.forceAffected;
        bullet = unitType.bullet;
        getHitSound = unitType.getHitSound;
        attackSound = unitType.attackSound;
        gruntSound = unitType.gruntSound;
              
        //Assign other stuff
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        whatIsEnemy = LayerMask.GetMask("Enemy");
        whatIsTower = LayerMask.GetMask("Tower");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!isDead)
        {
            //if 0 hp - destroy
            CheckHP();

            //check if an enemy or tower is in attackRange, depending on the unit
            if (enemy)
            {
                targetInAttackRange = Physics.Raycast(transform.position, transform.forward, attackRange, whatIsTower);
            }
            else
            {
                targetInAttackRange = Physics.Raycast(transform.position, transform.forward, attackRange, whatIsEnemy);
            }

            //set tower states
            bool towerInFront = Physics.Raycast(transform.position, transform.forward, 1, whatIsTower);
            if (((canMove && !canAttack)||(canMove && !targetInAttackRange)||(canMove && hasAttacked)) && !towerInFront) Move();
            else if (targetInAttackRange && canAttack && !hasAttacked) Attack();
            else Idle();
        }
    }

    //if 0 hp destroy
    public virtual void CheckHP()
    {
        if (hp <= 0)
        {
            if (!unitType.enemy)
            {
                //play sound
                FindObjectOfType<SoundScript>().Play("TowerDeath", 0.8f);
            }

            isDead = true;
            GridPlacing gp = FindAnyObjectByType<GridPlacing>();
            gp.UnitDead(this.transform.position);

            if (!unitType.enemy)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public virtual void Idle()
    {
        //play idle animation
    }

    public virtual void Move()
    {
        //play move animation

        //move forward
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
    }

    #region Attack
    public virtual void Attack()
    {
        //set attack animations

        //attack if canAttack is true
        if (canAttack && !hasAttacked)
        {
            Fire();
        }
    }

    //reset canAttack, set it to true
    public virtual void ResetAttack()
    {
        hasAttacked = false;
    }

    public virtual void Fire()
    {
        //play sound
        FindObjectOfType<SoundScript>().Play(attackSound, 0.5f);

        //instantiate a bullet
        Rigidbody rb = Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

        //add force to the bullet
        rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
        //add damage to the bullet
        rb.GetComponent<BulletScript>().damage = attackDamage;

        //prohibit attacking
        hasAttacked = true;
        //allow to attack again depending on the attack speed
        Invoke(nameof(ResetAttack), attackSpeed);
    }
    #endregion

    #region Upgrades
    public void OnMouseDown()
    {
        if (upgradeReady)
        {

        }
    }
    #endregion
}
