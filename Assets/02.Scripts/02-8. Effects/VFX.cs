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
    private void OnParticleSystemStopped()
    {
        Debug.Log("OnParticleSystemStopped");
        VFXPool.Instance.ReturnObject(this);
    }
    public void Init()
    {
    }
}
