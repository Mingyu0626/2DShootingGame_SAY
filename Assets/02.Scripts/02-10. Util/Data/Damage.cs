using UnityEngine;

public enum DamageType
{
    Bullet,
    Boom,
    InvincibleHeadBut
}

// 데미지를 추상화..
public struct Damage
{
    public int Value;
    public DamageType Type;
    public GameObject From;

    public Damage(int val, DamageType damageType, GameObject go)
    {
        Value = val;
        Type = damageType;
        From = go;
    }
}
