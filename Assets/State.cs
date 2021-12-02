using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class State
{
    static public int CurrentLevel { get; set; }
    static public int CurrentBlockCount { get; set; }

    static public int WinningGuess { get; set; }

    static public bool ShowWinningPanel { get; set; } = false;

    public static int getLastUnlockedLevel() {
        int val = PlayerPrefs.GetInt("level");
        if (val == 0) {
            return 1;
        }
        return val;
    }

    public static void updateLastUnlockedLevel(int level) {
        PlayerPrefs.SetInt("level", level);
    }
}
