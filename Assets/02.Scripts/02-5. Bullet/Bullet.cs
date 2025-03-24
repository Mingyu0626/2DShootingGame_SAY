using System.Collections;
using UnityEngine;
public abstract class Bullet : MonoBehaviour, IProduct
{
    [SerializeField] private BulletData _bulletData;
    public BulletData BulletData { get => _bulletData; private set => _bulletData = value; }

    private void OnEnable()
    {
        StartCoroutine(BulletDuration());
    }
    private void FixedUpdate()
    {
        Move();
        Snake();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) {}

    public void Init()
    {
        
    }

    public abstract void Move();

    private void Snake()
    {
        float offset = Mathf.Sin(Time.time * _bulletData.Frequency) * _bulletData.Amplitude;
        transform.position += transform.right * offset * Time.deltaTime;
    }

    private IEnumerator BulletDuration()
    {

        yield return new WaitForSeconds(_bulletData.Duration);
        BulletPool.Instance.ReturnObject(this);
        gameObject.SetActive(false);
    }
}
