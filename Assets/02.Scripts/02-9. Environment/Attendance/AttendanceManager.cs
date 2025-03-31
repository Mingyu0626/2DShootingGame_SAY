using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : Singleton<AttendanceManager>
{
    // 관리자(Manager)는, 추가 삭제 조회 정렬만 하면 된다.
    [SerializeField] 
    private List<AttendanceDataSO> _soDatas;

    private List<Attendance> _attendances;
    public List<Attendance> Attendances { get => _attendances; }

    // 출석 검증 데이터
    private DateTime _lastLoginDateTime = new DateTime(); // 마지막으로 로그인한 날짜 및 시간
    private int _attendanceCount = 0; // 현재까지의 출석 횟수

    // 데이터 변경시 호출되는 콜백
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
        // 오늘이, 마지막 로그인 날짜보다 크다면(하루 이상 지났다면)
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

        // 실제 출석을 그만큼 했는가?
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
