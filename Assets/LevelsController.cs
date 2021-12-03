using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        State.timer += Time.deltaTime;
    }

    public void goBack() {
        SceneManager.LoadScene("LevelsOverview");
    }

    public void SwitchInput() {
        String val = PlayerPrefs.GetString("Input");
        if (val == "perspectiveSwipe") {
            PlayerPrefs.SetString("Input", "continuousSwipe");
        } else if (val == "continuousSwipe") {
            PlayerPrefs.SetString("Input", "perspectiveSwipe");
        }
    }
}
