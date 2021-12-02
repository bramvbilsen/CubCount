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

    static public List<GameObject> shapeCubes = new List<GameObject>();

    static public int levelCount = 9;

    public static int getLastUnlockedLevel() {
        int val = PlayerPrefs.GetInt("level");
        if (val == 0) {
            return 1;
        }
        return val;
    }

    public static void updateLastUnlockedLevel(int level) {
        if (level > PlayerPrefs.GetInt("level")) {
            PlayerPrefs.SetInt("level", level);
        }
    }
}
