using UnityEngine;

public class VFX : MonoBehaviour, IProduct
{
    [SerializeField] private VFXType _vfxType;
    private ParticleSystem _particleSystem;

    public VFXType VfxType { get => _vfxType; set => _vfxType = value; }
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (_vfxType == VFXType.GetItemMoveSpeedUp || 
            _vfxType == VFXType.GetItemFireSpeedUp ||
            _vfxType == VFXType.GetItemHealthUp ||
            _vfxType == VFXType.GetItemMagnet)
        {
            transform.position = Player.Instance.transform.position;
        }
    }
    private void OnParticleSystemStopped()
    {
        VFXPool.Instance.ReturnObject(this);
    }
    public void Init()
    {
    }
}
