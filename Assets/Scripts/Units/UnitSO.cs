using UnityEngine;

[CreateAssetMenu(fileName = "Unit_SO", menuName = "Scriptable Objects/Unit")]
public class UnitSO : ScriptableObject
{
    #region Initialize Variables
    [Header("Stats")]
    public bool canMove;
    public float moveSpeed;
    public int hp;
    public float attackForce;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public int attackLanes;
    public int cost;

    [Header("Tags")]
    public bool enemy;
    public bool damagable;
    public bool forceAffected;

    [Header("Other")]
    public GameObject bullet;

    [Header("Sounds: put the name EXACTLY as in the SoundManager")]
    public string walkingSound;
    public string attackSound;
    public string getHitSound;
    #endregion
}
