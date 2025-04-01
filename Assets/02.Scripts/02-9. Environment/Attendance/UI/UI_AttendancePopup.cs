using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class UI_AttendancePopup : MonoBehaviour
{
    [SerializeField] private Button _attendancePopupOnButton;
    [SerializeField] private Button _attendancePopupOffButton;
    private void Awake()
    {
        _attendancePopupOnButton.onClick.AddListener(SetAttenDancePopupEnable);
        _attendancePopupOffButton.onClick.AddListener(SetAttenDancePopupDisable);
        SetAttenDancePopupDisable();
    }
    private void SetAttenDancePopupEnable()
    {
        gameObject.SetActive(true);
        _attendancePopupOnButton.interactable = false;
        Time.timeScale = 0f;
    }
    private void SetAttenDancePopupDisable()
    {
        gameObject.SetActive(false);
        _attendancePopupOnButton.interactable = true;
        Time.timeScale = 1f;
    }
}
