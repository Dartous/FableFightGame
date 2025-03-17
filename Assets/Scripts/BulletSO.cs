using UnityEngine;

[CreateAssetMenu(fileName = "Bullet_SO", menuName = "Scriptable Objects/Bullet")]
public class BulletSO : ScriptableObject
{
    public float destroyAfter;
    public int attackForce;
    public bool applyForce;
}
