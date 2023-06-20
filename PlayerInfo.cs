using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo
{
    public string Name;
    public int Score;

    public PlayerInfo()
    {
        Name = "Player";
        Score = 0;
    }

    public PlayerInfo(string name)
    {
        Name = name;
        Score = 0;
    }

    public PlayerInfo(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
