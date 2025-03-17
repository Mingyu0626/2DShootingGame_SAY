using UnityEngine;

public class BackGround : MonoBehaviour
{
    // 배경 스크롤링 : 배경 이미지를 일정한 속도로 움직여, 캐릭터나 몬스터 등의 움직임을 더 동적으로
    // 만들어주는 기술, 캐릭터는 그대로 두고 배경만 움직이는 '눈속임'

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
        // 방향을 구하고,
        Vector2 direction = Vector2.up;

        // 방향으로 스크롤링 한다.
        _bgMaterial.mainTextureOffset += direction * _scrollSpeed * Time.deltaTime;
    }

    // 패럴랙스 스크롤링 : 스크롤링엥 원근감을 주는 방식

}
