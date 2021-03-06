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
        if (guessText.color.g == 0) { // Currently text is red
            guessText.text = s;
        } else {
            guessText.text += s;
        }
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
        int blockCount = State.shapeCubes.Count;

        int d = State.getDifficulty();
        float difficulty = 0.1f;
        
        if (d == 2){
            difficulty = 0.15f;
        } else if (d == 3){
            difficulty = 0.20f;
        } else if (d == 4){
            difficulty = 0.25f;
        } 

        float maxOffset = blockCount * difficulty;
        State.nbTries++;
        if (guess >= blockCount - maxOffset && guess <= blockCount + maxOffset) {
            State.winningTime = State.timer; // Reset in the shape controller
            State.WinningGuess = guess;
            State.ShowWinningPanel = true;
            guessText.text = "";
        } else {
            guessText.color = new Color(255, 0, 0);
        }
        State.currentGuesses.Add(guess);
    }
 
}

    
