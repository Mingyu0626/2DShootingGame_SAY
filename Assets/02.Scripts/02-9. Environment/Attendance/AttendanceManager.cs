using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;

[Serializable]
public class AttendanceSaveData
{
    public int AttendanceCount;
    public string LastLoginDateTimeString;
    public List<bool> IsRewardedList = new List<bool>();
    public AttendanceSaveData(int attendanceCount, string lastLoginDateTimeString)
    {
        AttendanceCount = attendanceCount;
        LastLoginDateTimeString = lastLoginDateTimeString;
    }
}

public class AttendanceManager : Singleton<AttendanceManager>
{
    // 관리자(Manager)는, 추가 삭제 조회 정렬만 하면 된다.
    [SerializeField] 
    private List<AttendanceDataSO> _soDatas;

    private List<Attendance> _attendances;
    public List<Attendance> Attendances { get => _attendances; }

    // 출석 검증 데이터
    private AttendanceSaveData _saveData;
    private DateTime _lastLoginDateTime = new DateTime(); // 마지막으로 로그인한 날짜 및 시간
    private int _attendanceCount
    {
        get => _saveData.AttendanceCount;
        set => _saveData.AttendanceCount = value;
    }
    private const string SAVE_KEY = "Attendance";

    // 데이터 변경시 호출되는 콜백
    public Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();
        _attendances = new List<Attendance>(_soDatas.Count);
        Load();
        for (int i = 0; i < _soDatas.Count; i++)
        {
            _attendances.Add(new Attendance
                (_soDatas[i], _saveData.IsRewardedList[i]));
        }
        AttendanceCheck();
    }
    private void Start()
    {
        
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
        Save();
    }

    public bool TryGetReward(Attendance attendance)
    {
        if (attendance.IsRewarded)
        {
            return false;
        }
        if (_attendanceCount < attendance.Data.Day)
        {
            return false;
        }
        return true;
    }
    public void Save()
    {
        for (int i = 0; i < _attendances.Count; i++)
        {
            _saveData.IsRewardedList[i] = _attendances[i].IsRewarded;
        }
        _saveData.LastLoginDateTimeString = _lastLoginDateTime.ToString("o");
        string jsonData = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
    }
    private void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string jsonData = PlayerPrefs.GetString(SAVE_KEY);
            _saveData = JsonUtility.FromJson<AttendanceSaveData>(jsonData);
            if (!DateTime.TryParse(_saveData.LastLoginDateTimeString, out _lastLoginDateTime))
            {
                Debug.Log("변환 실패");
            }
        }
        else
        {
            _saveData = new AttendanceSaveData(0, DateTime.Now.ToString("o"));
            _saveData.IsRewardedList = new List<bool>(Enumerable.Repeat(false, _soDatas.Count));
        }
    }
}
