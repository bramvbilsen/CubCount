using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextController : MonoBehaviour
{

    public GameObject txt;

    public void SetText(string s){
        Text guessText = txt.GetComponent<Text>();
        guessText.text += s;
        guessText.color = new Color(255, 255, 255);
    }

    public void Backspace(){
        Text guessText = txt.GetComponent<Text>();
        guessText.color = new Color(255, 255, 255);

        if (guessText.text != "")
        {
            guessText.text = guessText.text.Remove(guessText.text.Length-1);
        }
    }

    public void Clear(){
        Text guessText = txt.GetComponent<Text>();
        guessText.color = new Color(255, 255, 255);
        guessText.text = "";
    }

    public void Submit(){
        Text guessText = txt.GetComponent<Text>();
        int guess = Int32.Parse(guessText.text);
        float maxOffset = State.CurrentBlockCount * 0.1f;
        if (guess >= State.CurrentBlockCount - maxOffset && guess <= State.CurrentBlockCount + maxOffset) {
            Debug.Log("Won");
            State.WinningGuess = guess;
            State.ShowWinningPanel = true;
        } else {
            guessText.color = new Color(255, 0, 0);
        }
    }
 
}

    
