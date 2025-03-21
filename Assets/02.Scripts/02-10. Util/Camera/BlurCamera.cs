using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurCamera : MonoBehaviour
{
    private Volume _globalVolume;
    private MotionBlur _motionBlur;
    private DepthOfField _depthOfField;
    private void Awake()
    {
        _globalVolume = GetComponent<Volume>();
        _globalVolume.profile.TryGet(out _motionBlur);
        _globalVolume.profile.TryGet(out _depthOfField);
    }

    public void SetMotionBlur(bool val)
    {
        if (!ReferenceEquals(_motionBlur, null))
        {
            _motionBlur.active = val;
        }
    }
    public void SetDepthOfField(bool val)
    {
        if (!ReferenceEquals(_depthOfField, null))
        {
            _depthOfField.active = val;
        }
    }
}
