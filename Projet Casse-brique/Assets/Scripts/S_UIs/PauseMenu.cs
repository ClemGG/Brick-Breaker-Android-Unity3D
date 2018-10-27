using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Clement.Utilities.Conversions;
using System;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseCanvas;
    public bool showGUI;
    public bool isGamePaused;


    private void Start()
    {
        pauseCanvas.SetActive(false);
    }



    public void PauseGame()
    {
        if (!ScoreManager.instance.isGameOver && !ScoreManager.instance.isGameWon)
            Pause(!showGUI);
    }
    public void RetryLevel()
    {
        ScoreManager.instance.previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneFader.instance.FadeToScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
        {
            ScoreManager.instance.previousSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
            SceneFader.instance.FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            ReturnToMenu();
    }
    public void ReturnToMenu()
    {
        SceneFader.instance.FadeToScene(0);
    }








    private void OnApplicationPause(bool pause)
    {
        if(!ScoreManager.instance.isGameOver && !ScoreManager.instance.isGameWon)
            Pause(pause);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!ScoreManager.instance.isGameOver && !ScoreManager.instance.isGameWon)
            Pause(!hasFocus);
    }





    private void Pause(bool b)
    {
        showGUI = b;
        pauseCanvas.SetActive(showGUI);
        Time.timeScale = (!showGUI).ToInt(false);
        isGamePaused = showGUI;

        //print(Time.timeScale);
    }
}
