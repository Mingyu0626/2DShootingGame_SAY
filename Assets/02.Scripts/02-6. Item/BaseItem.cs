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
        // 지금은 파생 클래스에서 Destroy 그냥 불렀는데,
        // 아이템 종류 많아지면 여기서 람다로 ApplyItem 종료 후 바로 파괴되도록 ㄱㄱ
    }

    public IEnumerator GoToPlayer()
    {
        float distance, duration;
        while (true)
        {
            distance = Vector2.Distance(transform.position, Player.Instance.transform.position);
            duration = distance / _moveSpeed;
            // 일반 직선 로직
            // transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position,
            //        _absorbSpeed * Time.deltaTime);

            // 두트윈이 맛이 갔어요
            // transform.DOMove(Player.Instance.transform.position, 1f / _absorbSpeed).SetEase(Ease.Linear);

            // 배지어 로직
            if (_controlVector == Vector2.zero)
            {
                _controlVector = (Player.Instance.transform.position + transform.position) / 2f +
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                // 아이템과 플레이어 사이의 중점에 반지름이 1인 원인 영역의 랜덤한 위치를 제어점으로 설정
            }

            _percent += Time.deltaTime / duration;
            transform.position = Bazier(
                transform.position, _controlVector, Player.Instance.transform.position, _percent);
            yield return null;
        }
    }


    // 매개변수 : 시작점, 제어점, 목적점, 시간(0~1)
    private Vector2 Bazier(Vector2 start, Vector2 center, Vector2 end, float time)
    {
        // 새로운 위치1 p1 = 시작점과 제어점 사이의 보간
        // 새로운 위치2 p2 = 시작점과 제어점 사이의 보간
        // 최종 위치 = 새로운 위치1 + 새로운 위치2
        Vector2 p1 = Vector2.Lerp(start, center, time);
        Vector2 p2 = Vector2.Lerp(center, end, time);
        Vector2 final = Vector2.Lerp(p1, p2, time);
        return final;
    }
}
