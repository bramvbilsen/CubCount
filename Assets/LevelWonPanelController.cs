using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelWonPanelController : MonoBehaviour
{

    bool displayed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!displayed && State.ShowWinningPanel) {
            displayed = true;
            int cubeCount = State.shapeCubes.Count;
            foreach (GameObject cube in State.shapeCubes) {
                Destroy(cube);
            }
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Text yourGuessTxt = GameObject.Find("YourGuessTxt").GetComponent<Text>();
            yourGuessTxt.text =  "Your guess: " + State.WinningGuess;
            Text actualAmountTxt = GameObject.Find("ActualAmountTxt").GetComponent<Text>();
            actualAmountTxt.text = "Actual amount: " + cubeCount;
        }
    }

    public void goToNextLevel() {
        if (State.levelCount > State.getLastUnlockedLevel()) {
            State.updateLastUnlockedLevel(State.CurrentLevel+1);
        }
        State.CurrentLevel+=1;
        if (State.CurrentLevel >= State.levelCount) {
            SceneManager.LoadScene("HomePage");
        }
        State.ShowWinningPanel = false;
        displayed = false;
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
