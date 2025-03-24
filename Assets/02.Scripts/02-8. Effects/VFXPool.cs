using UnityEngine;
using UnityEngine.VFX;

public class VFXPool : ObjectPool<VFXType, VFX>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
