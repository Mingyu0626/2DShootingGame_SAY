using Coffee.UIExtensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Attendance : MonoBehaviour
{
    [SerializeField]
    private int _attendanceDay;
    [SerializeField]
    private TextMeshProUGUI _rewardAmountTMP;
    [SerializeField]
    private Image _rewardImage;
    [SerializeField]
    private List<Sprite> _rewardSprite;
    private Attendance _attendance;

    [SerializeField]
    private Button _getRewardButton;

    private ShinyEffectForUGUI _particleEffect;
    private float _effectDuration = 1f;
    private void Awake()
    {
        _getRewardButton.onClick.AddListener(GetReward);
        _particleEffect = GetComponent<ShinyEffectForUGUI>();
    }
    private void Start()
    {
        _attendance = AttendanceManager.Instance.Attendances[_attendanceDay];
        InitUI();
    }

    private void InitUI()
    {
        _rewardImage.sprite = _rewardSprite[(int)_attendance.Data.RewardCurrencyType];
        _rewardAmountTMP.text = _attendance.Data.RewardAmount.ToString();

        if (AttendanceManager.Instance.TryGetReward(_attendance))
        {
            _getRewardButton.interactable = true;
        }
    }

    private void GetReward()
    {
        CurrencyManager.Instance.Add
            (_attendance.Data.RewardCurrencyType, _attendance.Data.RewardAmount);
        _attendance.IsRewarded = true;
        _getRewardButton.interactable = false;
        AttendanceManager.Instance.Save();
        ActivateParticleEffect();
    }

    private void ActivateParticleEffect()
    {
        _particleEffect.Play(_effectDuration);
    }


}
