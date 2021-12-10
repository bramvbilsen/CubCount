using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPageController : MonoBehaviour
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
        SceneManager.LoadScene(State.lastScene);
    }

    public void ResetLevels() {
        State.ResetLevels();
        SceneManager.LoadScene("HomePage");
    }

    public void pauseMusic(){
        if(MusicBackground.Instance.gameObject.GetComponent<AudioSource>().isPlaying){
            MusicBackground.Instance.gameObject.GetComponent<AudioSource>().Pause();
        } else{
            MusicBackground.Instance.gameObject.GetComponent<AudioSource>().UnPause();
        }
    }
    public void startMusic(){
        MusicBackground.Instance.gameObject.GetComponent<AudioSource>().UnPause();
    }
}
