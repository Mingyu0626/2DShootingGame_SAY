using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private BaseItem _baseItem;
    private void Awake()
    {
        _baseItem = GetComponentInParent<BaseItem>();
        if (ReferenceEquals(_baseItem, null))
        {
            Debug.Log("null ������ �ȵǴµ�??����");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�ڽ� OnTriggerEnter2D");
        StartCoroutine(_baseItem.GoToPlayer());
    }

    // Ʈ���ſ��� �ݶ��̴� ���� ���
    // �������� 1����
    // �� 1. ���� ������Ʈ�� �����, �ش� ������Ʈ�� ������ٵ�� �ݶ��̴��� �߰������ν� ���� �����Ѵ�.
    // �� 2. GetComponent<��ü Ŭ����>�� ���ؼ� null �˻�
    // �� 3. Physics2D / ������ ��� -> ��������
}
