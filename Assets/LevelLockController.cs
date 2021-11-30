using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

[System.Serializable]
public class LevelLockController : MonoBehaviour
{
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var img = this.gameObject.GetComponent<Image>();
        var color = img.color;
        if (State.getLastUnlockedLevel() < level) {
            color.a = 1f;
        } else {
            color.a = 0f;
        }
        img.color = color;
    }
}
