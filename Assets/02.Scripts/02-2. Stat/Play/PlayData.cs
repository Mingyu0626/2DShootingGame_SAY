using System;
using UnityEngine;

[Serializable]
public class PlayData
{
    public int Score;
    public int KillCount;
    public int BoomCount;
    public int Gold;

    public PlayData(int score, int killCount, int boomCount, int gold)
    {
        Score = score;
        KillCount = killCount;
        BoomCount = boomCount;
        Gold = gold;
    }
}
