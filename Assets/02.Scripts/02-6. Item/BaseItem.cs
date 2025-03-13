using System.Collections;
using UnityEditor;
using UnityEngine;

public interface IItem
{

}

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField]
    private float _amount;
    public float Amount { get => _amount; private set => _amount = value; }

    private Coroutine _timerCoroutine;
    private const float _collisionTimeToApply = 1f;
    public float CollisionTimeToApply { get => _collisionTimeToApply; }

    [SerializeField]
    private float _minDistanceToAbsorb = 1f;
    [SerializeField]
    private float _absorbSpeed = 0.5f;

    private PlayerData _playerData;
    protected PlayerData PlayerData { get => _playerData; }
    private void Awake()
    {
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }
    private void Update()
    {
        AbsorbedByPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _timerCoroutine = StartCoroutine(GetItemTimer());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_timerCoroutine == null) return;

        StopCoroutine(_timerCoroutine);
        _timerCoroutine = null;
    }
    public abstract void ApplyItem();

    private IEnumerator GetItemTimer()
    {
        yield return new WaitForSeconds(_collisionTimeToApply);
        ApplyItem();
        // 지금은 파생 클래스에서 Destroy 그냥 불렀는데,
        // 아이템 종류 많아지면 여기서 람다로 ApplyItem 종료 후 바로 파괴되도록 ㄱㄱ
    }

    private void AbsorbedByPlayer()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= _minDistanceToAbsorb)
        {
            Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
            transform.Translate(direction * _absorbSpeed * Time.deltaTime, Space.World);
            // 두트윈이 맛이 갔어요
            // transform.DOMove(Player.Instance.transform.position, 1f / _absorbSpeed).SetEase(Ease.Linear);
        }
    }
}
