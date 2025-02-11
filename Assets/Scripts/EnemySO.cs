using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_SO", menuName = "Scriptable Objects/Enemy_SO")]
public class UnitSO : ScriptableObject
{
    #region Initialize Variables
    [Header("Stats")]
    public int moveSpeed;
    public int hp;
    public float attackForce;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    [Header("Tags")]
    public bool damagable;
    public bool forceAffected;

    [Header("Other")]
    public GameObject bullet;
    #endregion
}
