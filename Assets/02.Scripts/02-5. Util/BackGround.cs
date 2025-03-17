using UnityEngine;

public class BackGround : MonoBehaviour
{
    // ��� ��ũ�Ѹ� : ��� �̹����� ������ �ӵ��� ������, ĳ���ͳ� ���� ���� �������� �� ��������
    // ������ִ� ���, ĳ���ʹ� �״�� �ΰ� ��游 �����̴� '������'

    [SerializeField] private Material _bgMaterial;
    [SerializeField] float _scrollSpeed = 0.1f;
    private void Awake()
    {
        // _bgMaterial = GetComponent<Material>();
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

}
