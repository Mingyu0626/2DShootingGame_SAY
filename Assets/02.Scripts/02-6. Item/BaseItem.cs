using System.Collections;
using System.Runtime.CompilerServices;
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
    private const float _collisionTimeToApply = 0.5f;
    public float CollisionTimeToApply { get => _collisionTimeToApply; }

    [SerializeField] private float _minDistanceToAbsorb = 1f;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _disappearTime = 3f;


    private PlayerData _playerData;
    protected PlayerData PlayerData { get => _playerData; }

    [SerializeField] private GameObject _getItemVFXPrefab;


    private float _percent = 0f;
    private Vector2 _controlVector = Vector2.zero;
    private void Awake()
    {
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        Destroy(gameObject, _disappearTime);
    }
    private void Update()
    {
        
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

    public void PlayVFX()
    {
        Instantiate(_getItemVFXPrefab, transform.position, transform.rotation);
    }

    private IEnumerator GetItemTimer()
    {
        yield return new WaitForSeconds(_collisionTimeToApply);
        PlayVFX();
        ApplyItem();
        // ������ �Ļ� Ŭ�������� Destroy �׳� �ҷ��µ�,
        // ������ ���� �������� ���⼭ ���ٷ� ApplyItem ���� �� �ٷ� �ı��ǵ��� ����
    }

    public IEnumerator GoToPlayer()
    {
        float distance, duration;
        while (true)
        {
            distance = Vector2.Distance(transform.position, Player.Instance.transform.position);
            duration = distance / _moveSpeed;
            // �Ϲ� ���� ����
            // transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position,
            //        _absorbSpeed * Time.deltaTime);

            // ��Ʈ���� ���� �����
            // transform.DOMove(Player.Instance.transform.position, 1f / _absorbSpeed).SetEase(Ease.Linear);

            // ������ ����
            if (_controlVector == Vector2.zero)
            {
                _controlVector = (Player.Instance.transform.position + transform.position) / 2f +
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                // �����۰� �÷��̾� ������ ������ �������� 1�� ���� ������ ������ ��ġ�� ���������� ����
            }

            _percent += Time.deltaTime / duration;
            transform.position = Bazier(
                transform.position, _controlVector, Player.Instance.transform.position, _percent);
            yield return null;
        }
    }


    // �Ű����� : ������, ������, ������, �ð�(0~1)
    private Vector2 Bazier(Vector2 start, Vector2 center, Vector2 end, float time)
    {
        // ���ο� ��ġ1 p1 = �������� ������ ������ ����
        // ���ο� ��ġ2 p2 = �������� ������ ������ ����
        // ���� ��ġ = ���ο� ��ġ1 + ���ο� ��ġ2
        Vector2 p1 = Vector2.Lerp(start, center, time);
        Vector2 p2 = Vector2.Lerp(center, end, time);
        Vector2 final = Vector2.Lerp(p1, p2, time);
        return final;
    }
}
