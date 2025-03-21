using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundData
{
    [SerializeField] private GameObject _backgroundGO;
    [SerializeField] private float _scrollSpeed;
    private Vector2 _offset;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    public GameObject BackgroundGO { get => _backgroundGO; set => _backgroundGO = value; }
    public float ScrollSpeed { get => _scrollSpeed; set => _scrollSpeed = value; }
    public Vector2 Offset { get => _offset; set => _offset = value; }
    public Renderer Renderer { get => _renderer; set => _renderer = value; }
    public MaterialPropertyBlock MaterialPropertyBlock { get => _materialPropertyBlock; set => _materialPropertyBlock = value; }
}


public class BackGround : MonoBehaviour
{
    // 배경 스크롤링 : 배경 이미지를 일정한 속도로 움직여, 캐릭터나 몬스터 등의 움직임을 더 동적으로
    // 만들어주는 기술, 캐릭터는 그대로 두고 배경만 움직이는 '눈속임'
    [SerializeField] private List<BackgroundData> _backgroundDatas;
    [SerializeField] private float _speedIncreaseAmount;
    public List<BackgroundData> BackgroundDatas { get => _backgroundDatas; set => _backgroundDatas = value; }

    private void Awake()
    {
        foreach (BackgroundData backgroundData in BackgroundDatas)
        {
            backgroundData.Renderer = backgroundData.BackgroundGO.GetComponent<Renderer>();
            backgroundData.MaterialPropertyBlock = new MaterialPropertyBlock();
        }
    }

    private void Update()
    {
        ScrollBackGround();
    }

    private void ScrollBackGround()
    {
        foreach (BackgroundData backgroundData in BackgroundDatas)
        {
            // 방향을 구하고, 해당 방향으로 스크롤링 한다.
            Vector2 direction = Vector2.up;
            backgroundData.Offset += direction * backgroundData.ScrollSpeed * Time.deltaTime;

            // MPB에 변경값을 담고, Renderer의 Material에 해당 MPB를 적용한다.
            backgroundData.MaterialPropertyBlock.SetVector("_BaseMap_ST",
            new Vector4(1, 1, backgroundData.Offset.x, backgroundData.Offset.y));
            backgroundData.Renderer.SetPropertyBlock(backgroundData.MaterialPropertyBlock);
        }
    }

    public void ScrollSpeedUp()
    {
        foreach (BackgroundData backgroundData in BackgroundDatas)
        {
            backgroundData.ScrollSpeed *= _speedIncreaseAmount;
        }
    }
    public void ScrollSpeedDown()
    {
        foreach (BackgroundData backgroundData in BackgroundDatas)
        {
            backgroundData.ScrollSpeed /= _speedIncreaseAmount;
        }
    }
}
