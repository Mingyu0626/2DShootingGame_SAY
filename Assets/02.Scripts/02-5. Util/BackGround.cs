using UnityEngine;

public class BackGround : MonoBehaviour
{
    // ��� ��ũ�Ѹ� : ��� �̹����� ������ �ӵ��� ������, ĳ���ͳ� ���� ���� �������� �� ��������
    // ������ִ� ���, ĳ���ʹ� �״�� �ΰ� ��游 �����̴� '������'
    [SerializeField] float _scrollSpeed;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private Vector2 _offset;
    private void Awake()
    {
        // ���� Material�� ���纻�� �����Ͽ� ��ȯ, ������ �����ϰ� �ʹٸ�, sharedMaterial�� ����Ѵ�.
        _renderer = gameObject.GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
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
        _offset += direction * _scrollSpeed * Time.deltaTime;

        // MPB�� ���氪�� ���, Renderer�� Material�� �ش� MPB�� �����Ѵ�.
        _materialPropertyBlock.SetVector("_BaseMap_ST", 
            new Vector4(1, 1, _offset.x, _offset.y));
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    // �з����� ��ũ�Ѹ� : ��ũ�Ѹ��� ���ٰ��� �ִ� ���
    // �������� ����� ���ĵΰ�, �� ����ϼ��� ��ũ�Ѹ� �ӵ��� �۰�, ����� ����ϼ���
    // ��ũ�Ѹ� �ӵ��� ũ���Ͽ� ����
}
