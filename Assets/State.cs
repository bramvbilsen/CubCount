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

    static public List<int> currentGuesses = new List<int>();

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
        State.CurrentLevel = 1;
    }

    public static String getUserID(){

        String id = PlayerPrefs.GetString("id");

        if ( id == ""){
            System.Guid myGUID = System.Guid.NewGuid();
            PlayerPrefs.SetString("id", myGUID.ToString());
            id = myGUID.ToString();
        }

        Debug.Log("ID :" + id);
        return id;
    }


    public static int getDifficulty(){

        int val = PlayerPrefs.GetInt("difficulty");
        
        if ( val == 0 ){
            System.Random rnd = new System.Random();
            val = rnd.Next(1,5);
            PlayerPrefs.SetInt("difficulty", val);
        }

        return val;
    }
}
