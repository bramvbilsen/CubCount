using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsOverviewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goBack() {
        SceneManager.LoadScene("HomePage");
    }

    public void goToLevel(int level){
        if (State.getLastUnlockedLevel() >= level) {
            State.CurrentLevel = level;
            SceneManager.LoadScene("Levels");
        }
    }
}
