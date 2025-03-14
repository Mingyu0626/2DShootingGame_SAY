using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private BaseItem _baseItem;
    private void Awake()
    {
        _baseItem = GetComponentInParent<BaseItem>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ReferenceEquals(_baseItem, null))
        {
            StartCoroutine(_baseItem.GoToPlayer());
        }
    }

    // 트리거에서 콜라이더 구별 방법
    // 나같으면 1번함
    // ㄴ 1. 하위 오브젝트를 만들고, 해당 오브젝트에 리지드바디와 콜라이더를 추가함으로써 따로 구현한다.
    // ㄴ 2. GetComponent<구체 클래스>를 통해서 null 검사
    // ㄴ 3. Physics2D / 오버랩 방식 -> 개귀찮음
}
