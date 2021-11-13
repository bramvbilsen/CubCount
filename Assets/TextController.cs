using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{

    public GameObject txt;

    public void SetText(string s){
        Text guessText = txt.GetComponent<Text>();
        guessText.text += s;
    }

    public void Backspace(){
        Text guessText = txt.GetComponent<Text>();

        if (guessText.text != "")
        {
            guessText.text = guessText.text.Remove(guessText.text.Length-1);
        }
    }

    public void Clear(){
        Text guessText = txt.GetComponent<Text>();
        guessText.text = "";
    }

    public void Submit(){
        Debug.Log("Submitting guess");
    }
 
}

    
