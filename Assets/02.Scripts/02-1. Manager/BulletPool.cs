using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<BulletType, Bullet>
{
    protected override void Awake()
    {
        base.Awake();
    }


    //[SerializeField] private int _poolSize = 30;
    //[SerializeField] private List<Bullet> _bulletPrefabs;
    //private List<Bullet> _bullets;
    //public int PoolSize { get => _poolSize; private set => _poolSize = value; }
    //public List<Bullet> BulletPrefabs { get => _bulletPrefabs; set => _bulletPrefabs = value; }
    //public List<Bullet> Bullets { get => _bullets; set => _bullets = value; }

    //protected override void Awake()
    //{
    //    base.Awake();
    //    int bulletPrefabCount = _bulletPrefabs.Count;
    //    _bullets = new List<Bullet>(_poolSize * bulletPrefabCount);

    //    foreach (Bullet bulletPrefab in _bulletPrefabs)
    //    {
    //        for (int i = 0; i < _poolSize; i++)
    //        {
    //            Bullet bullet = Instantiate(bulletPrefab, this.transform);
    //            _bullets.Add(bullet);
    //            bullet.Init();
    //            bullet.gameObject.SetActive(false);
    //        }
    //    }
    //}
    //public Bullet GetBullet(BulletType bulletType, Vector3 position)
    //{
    //    foreach (Bullet bullet in _bullets)
    //    {
    //        if (bullet.CurrentBulletType == bulletType && !bullet.gameObject.activeInHierarchy)
    //        {
    //            bullet.transform.position = position;
    //            bullet.Init();
    //            bullet.gameObject.SetActive(true);
    //            return bullet;
    //        }
    //    }
    //    return null;
    //}
}
