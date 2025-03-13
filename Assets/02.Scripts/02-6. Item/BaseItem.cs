using System.Collections;
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
    private float _minDistanceToAbsorb;

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
        // ������ �Ļ� Ŭ�������� Destroy �׳� �ҷ��µ�,
        // ������ ���� �������� ���⼭ ���ٷ� ApplyItem ���� �� �ٷ� �ı��ǵ��� ����
    }

    private void AbsorbedByPlayer()
    {
        if (_minDistanceToAbsorb <= Vector2.Distance(transform.position, Player.Instance.transform.position))
        {
            // �÷��̾�� �����ϴ� ����
        }
    }
}
