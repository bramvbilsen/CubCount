using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelWonPanelController : MonoBehaviour
{

    bool displayed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
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
            string winningTime = (State.winningTime % 60).ToString().Replace(',', '.');
            string json = string.Format(
                    "{{\"level\": {0}, \"time\": {1}, \"tries\": {2}, \"id\": \"{3}\", \"difficulty\": {4}, \"guesses\":{5}}}",
                    State.CurrentLevel,
                    winningTime,
                    State.nbTries,
                    State.getUserID(),
                    State.getDifficulty(),
                    "[" + string.Join( ",", State.currentGuesses.ToArray()) + "]"
                    // (State.getInputMethod() == InputMethod.CONTINUOUS_SWIPE).ToString().ToLower()
                );
            Debug.Log(json);
            StartCoroutine(PostRequest(
                // "https://c9d2d3382a7848.localhost.run/levelCompletion",
                "https://ugly-treefrog-40.loca.lt/levelCompletion",
                json
            ));
        }
    }

    public void goToNextLevel() {
        if (State.levelCount > State.getLastUnlockedLevel()) {
            State.updateLastUnlockedLevel(State.CurrentLevel+1);
        }
        State.CurrentLevel+=1;
        if (State.CurrentLevel > State.levelCount) {
            SceneManager.LoadScene("HomePage");
        }
        State.ShowWinningPanel = false;
        displayed = false;
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
