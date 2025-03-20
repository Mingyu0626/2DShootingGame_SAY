using System;
using UnityEngine;

[Serializable]
public class PlayData
{
    public int Score;
    public int KillCount;
    public int BoomCount;

    public PlayData(int score, int killCount, int boomCount)
    {
        Score = score;
        KillCount = killCount;
        BoomCount = boomCount;
    }
}
