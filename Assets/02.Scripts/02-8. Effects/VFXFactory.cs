using UnityEngine;

public enum VFXType
{
    GetItemFireSpeedUp,
    GetItemHealthUp,
    GetItemMoveSpeedUp,
    GetItemMagnet,
    DestroyEnemy,
    DestroyBoss,
    PlayerBoom,
    PlayerInvincibleMode
}

public class VFXFactory : Factory<VFX>
{
    public override VFX GetProduct(GameObject vfxGO, Vector3 position)
    {
        return Instantiate(vfxGO, position, Quaternion.identity)
            .GetComponent<VFX>();

    }
}
