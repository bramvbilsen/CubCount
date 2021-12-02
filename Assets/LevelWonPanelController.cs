using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWonPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (State.ShowWinningPanel) {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Text yourGuessTxt = GameObject.Find("YourGuessTxt").GetComponent<Text>();
            yourGuessTxt.text =  "Test";
        }
    }
}
