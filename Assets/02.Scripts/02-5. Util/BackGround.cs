using UnityEngine;

public class BackGround : MonoBehaviour
{
    // 배경 스크롤링 : 배경 이미지를 일정한 속도로 움직여, 캐릭터나 몬스터 등의 움직임을 더 동적으로
    // 만들어주는 기술, 캐릭터는 그대로 두고 배경만 움직이는 '눈속임'
    [SerializeField] float _scrollSpeed;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private Vector2 _offset;
    private void Awake()
    {
        // 원본 Material의 복사본을 생성하여 반환, 원본을 참조하고 싶다면, sharedMaterial을 사용한다.
        _renderer = gameObject.GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        ScrollBackGround();
    }

    private void ScrollBackGround()
    {
        // 방향을 구하고,
        Vector2 direction = Vector2.up;
        // 방향으로 스크롤링 한다.
        _offset += direction * _scrollSpeed * Time.deltaTime;

        // MPB에 변경값을 담고, Renderer의 Material에 해당 MPB를 적용한다.
        _materialPropertyBlock.SetVector("_BaseMap_ST", 
            new Vector4(1, 1, _offset.x, _offset.y));
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    // 패럴랙스 스크롤링 : 스크롤링엥 원근감을 주는 방식
    // 여러개의 배경을 겹쳐두고, 먼 배경일수록 스크롤링 속도를 작게, 가까운 배경일수록
    // 스크롤링 속도를 크게하여 구현
}
