using UnityEngine;

public enum DamageType
{
    Bullet,
    Boom
}

// �������� �߻�ȭ..
public struct Damage
{
    public int Value;
    public DamageType Type;
    public GameObject From;
}
