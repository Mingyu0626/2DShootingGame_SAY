using UnityEngine;

public enum BulletType
{
    Main,
    Sub,
    Pet,
    Boss
}
public class BulletFactory : Factory<Bullet>
{
    public override Bullet GetProduct(GameObject bulletGO, Vector3 position)
    {
        return Instantiate(bulletGO, position, Quaternion.identity)
            .GetComponent<Bullet>();
    }
}
