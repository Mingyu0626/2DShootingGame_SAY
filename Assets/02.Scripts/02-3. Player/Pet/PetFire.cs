using System.Collections;
using UnityEngine;

public class PetFire : MonoBehaviour
{
    [Header("Bullet")]
    [Tooltip("메인 총알 Prefab")]
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Muzzle")]
    [Tooltip("총알이 발사되는 총구 Transform")]
    [SerializeField] private Transform[] _muzzleTransformArray;

    [Header("CoolDown")]
    [Tooltip("총알의 발사 대기시간(쿨타임)")]
    [SerializeField] private float _coolTime = 3F;
    private bool _isCoolDown = false;
    [SerializeField] private int _damage = 1;
    
    private void Awake()
    {

    }
    private void Update()
    {
        Fire();
    }
    private void Fire()
    {
        if (!_isCoolDown)
        {
            foreach (Transform muzzleTransform in _muzzleTransformArray)
            {
                BulletPool.Instance.GetObject(BulletType.Main, transform.position);
            }
            StartCoroutine(CoolDown(_coolTime));
        }
    }

    private IEnumerator CoolDown(float coolTime)
    {
        _isCoolDown = true;
        yield return new WaitForSeconds(coolTime);
        _isCoolDown = false;
    }
}
