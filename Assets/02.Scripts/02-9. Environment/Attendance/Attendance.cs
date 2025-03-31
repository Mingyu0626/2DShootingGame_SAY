using UnityEngine;

public class Attendance
{
    // 출석 데이터
    public readonly AttendanceDataSO Data;

    // 보상 받기 유무
    private bool _isRewarded;
    public bool IsRewarded { get => _isRewarded; set => _isRewarded = value; }

    public Attendance(AttendanceDataSO data, bool rewarded)
    {
        Data = data;
        _isRewarded = rewarded;
    }

}
