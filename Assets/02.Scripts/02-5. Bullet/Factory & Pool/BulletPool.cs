using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<BulletType, Bullet>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
