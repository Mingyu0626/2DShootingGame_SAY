using UnityEngine;

public class Attendance
{
    // �⼮ ������
    public readonly AttendanceDataSO Data;

    // ���� �ޱ� ����
    private bool _isRewarded;
    public bool IsRewarded { get => _isRewarded; set => _isRewarded = value; }

    public Attendance(AttendanceDataSO data, bool rewarded)
    {
        Data = data;
        _isRewarded = rewarded;
    }

}
