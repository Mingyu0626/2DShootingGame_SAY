using UnityEngine;

public class BackGround : MonoBehaviour
{
    // ��� ��ũ�Ѹ� : ��� �̹����� ������ �ӵ��� ������, ĳ���ͳ� ���� ���� �������� �� ��������
    // ������ִ� ���, ĳ���ʹ� �״�� �ΰ� ��游 �����̴� '������'
    [SerializeField] float _scrollSpeed;
    private Material _bgMaterial;
    private void Awake()
    {
        // ���� Material�� ���纻�� �����Ͽ� ��ȯ
        _bgMaterial = gameObject.GetComponent<Renderer>().material;

        // ������ �����ϰ� �ʹٸ�, sharedMaterial�� ����Ѵ�.

    }

    private void Update()
    {
        ScrollBackGround();
    }

    private void ScrollBackGround()
    {
        // ������ ���ϰ�,
        Vector2 direction = Vector2.up;

        // �������� ��ũ�Ѹ� �Ѵ�.
        _bgMaterial.mainTextureOffset += direction * _scrollSpeed * Time.deltaTime;
    }

    // �з����� ��ũ�Ѹ� : ��ũ�Ѹ��� ���ٰ��� �ִ� ���
    // �������� ����� ���ĵΰ�, �� ����ϼ��� ��ũ�Ѹ� �ӵ��� �۰�, ����� ����ϼ���
    // ��ũ�Ѹ� �ӵ��� ũ���Ͽ� ����
}
