using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMethod {
    CONTINUOUS_SWIPE,
    PERSPECTIVE_SWIPE
}

public static class State
{
    static public int CurrentLevel { get; set; }
    static public int CurrentBlockCount { get; set; }

    static public int WinningGuess { get; set; }

    static public bool ShowWinningPanel { get; set; } = false;

    static public int nbTries { get; set; } = 0;

    static public float timer = 0.0f;

    static public float winningTime = 0.0f;

    static public List<GameObject> shapeCubes = new List<GameObject>();

    static public int levelCount = 9;

    static public String lastScene = "HomePage";

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

    public static void ResetLevels() {
        PlayerPrefs.SetInt("level", 1);
    }

    public static void assignInputMethod() {
        System.Random rnd = new System.Random();
        int val = rnd.Next(0, 2);
        PlayerPrefs.SetString("Input", val == 0 ? "perspectiveSwipe" : "continuousSwipe");
    }

    public static InputMethod getInputMethod() {
        String val = PlayerPrefs.GetString("Input");
        if (val == "perspectiveSwipe") {
            return InputMethod.PERSPECTIVE_SWIPE;
        } else if (val == "continuousSwipe") {
            return InputMethod.CONTINUOUS_SWIPE;
        } else {
            assignInputMethod();
            return getInputMethod();
        }
    }
}
