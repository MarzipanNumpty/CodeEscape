using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    AsyncOperation loadScene;
    public GameObject loadingScreen;
    public bool startLoad;
    public Text percenText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("audio", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("audio") == 0)
        {
            audio.Stop();
        }

        if (startLoad) //used for loading screen
        {
            float progressValue = Mathf.Clamp01(loadScene.progress / 0.9f);

            percenText.text = Mathf.Round(progressValue * 100) + "%";
        }
    }

    //Starts the video before the gameplay
    public void Play()
    {
        loadScene = SceneManager.LoadSceneAsync(1);
        loadingScreen.SetActive(true);
        startLoad = true;
    }

    //Changes to the tutorial scene
    public void Tutorial()
    {
        Application.LoadLevel(2);
    }

    //Mutes Game
    public void Mute()
    {
        PlayerPrefs.SetInt("audio", 0);
    }

    //Unmutes Game
    public void Unmute()
    {
        PlayerPrefs.SetInt("audio", 1);
        if (PlayerPrefs.GetInt("audio") == 1)
        {
            audio.Play();
        }
    }

    //Shuts down application
    public void Quit()
    {
        Application.Quit();
    }
}
