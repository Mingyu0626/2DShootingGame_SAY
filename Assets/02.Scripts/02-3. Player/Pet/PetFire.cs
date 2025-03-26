using System.Collections;
using UnityEngine;

public class PetFire : MonoBehaviour
{
    [Header("Bullet")]
    [Tooltip("���� �Ѿ� Prefab")]
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Muzzle")]
    [Tooltip("�Ѿ��� �߻�Ǵ� �ѱ� Transform")]
    [SerializeField] private Transform[] _muzzleTransformArray;

    [Header("CoolDown")]
    [Tooltip("�Ѿ��� �߻� ���ð�(��Ÿ��)")]
    [SerializeField] private float _coolTime = 3F;
    private bool _isCoolDown = false;
    
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
