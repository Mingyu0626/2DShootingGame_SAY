using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : Singleton<AttendanceManager>
{
    // ������(Manager)��, �߰� ���� ��ȸ ���ĸ� �ϸ� �ȴ�.
    [SerializeField] 
    private List<AttendanceDataSO> _soDatas;

    private List<Attendance> _attendances;
    public List<Attendance> Attendances { get => _attendances; }

    // �⼮ ���� ������
    private DateTime _lastLoginDateTime = new DateTime(); // ���������� �α����� ��¥ �� �ð�
    private int _attendanceCount = 0; // ��������� �⼮ Ƚ��

    // ������ ����� ȣ��Ǵ� �ݹ�
    public Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();
        _attendances = new List<Attendance>(_soDatas.Count); 
        for (int i = 0; i < _soDatas.Count; i++)
        {
            _attendances[i] = new Attendance(_soDatas[i], false);
        }
        AttendanceCheck();
    }
    private void AttendanceCheck()
    {
        DateTime today = DateTime.Today;
        // ������, ������ �α��� ��¥���� ũ�ٸ�(�Ϸ� �̻� �����ٸ�)
        if (_lastLoginDateTime < today) 
        {
            _lastLoginDateTime = today;
            _attendanceCount += 1;
        }
    }

    public bool TryGetReward(Attendance attendance)
    {
        if (attendance.IsRewarded)
        {
            return false;
        }

        // ���� �⼮�� �׸�ŭ �ߴ°�?
        if (attendance.Data.Day < _attendanceCount)
        {
            return false;
        }


        CurrencyManager.Instance.Add
            (attendance.Data.RewardCurrencyType, attendance.Data.RewardAmount);
        attendance.IsRewarded = true;


        OnDataChanged?.Invoke();
        return true;
    }
}
