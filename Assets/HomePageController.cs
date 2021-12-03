using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class HomePageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        State.ResetLevels();
        State.CurrentLevel = 1;
        State.getInputMethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToLastLevel() {
        Debug.Log(State.getLastUnlockedLevel());
        SceneManager.LoadScene("Levels");
    }

    public void goToLevelOverview() {
        SceneManager.LoadScene("LevelsOverview");
    }

    public void goToSettings() {
        SceneManager.LoadScene("SettingsPage");
    }
}
