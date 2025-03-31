using UnityEngine;
using UnityEngine.UI;

public class UI_Attendance : MonoBehaviour
{
    [SerializeField]
    private Button _attendanceButton;
    [SerializeField]
    private GameObject _attendancePopup;
    [SerializeField]
    private 

    private void Awake()
    {
        _attendanceButton.onClick.AddListener(SetAttenDancePopupEnable);
    }


    private void SetAttenDancePopupEnable()
    {
        _attendancePopup.SetActive(true);
    }

}
