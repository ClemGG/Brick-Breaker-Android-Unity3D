using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour {



    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentLivesText;
    
    [SerializeField] private GameObject gameOverPrompt;
    [SerializeField] private GameObject winPrompt;


    public int previousSceneIndex;
    public bool isGameOver;
    public bool isGameWon;


    [SerializeField] private int score;
    private int scoreForThisLevel;
    private int highScore;
    private int currentLives;
    [SerializeField] private int startLives = 5;





    public List<Brique> briques;

    public static ScoreManager instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Erreur : Il y a plus d'un script ScoreManager dans la scène.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        briques = new List<Brique>();
        DontDestroyOnLoad(gameObject);
    }



    // Use this for initialization
    void Start () {
        
        currentLives = startLives;

        if(PlayerPrefs.GetInt("High Score") > 0)
        {
            highScore = PlayerPrefs.GetInt("High Score");
        }


        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        currentLivesText.text = currentLives.ToString();
        
    }










    public void AddScore(int pts)
    {
        scoreForThisLevel += pts;
        if(scoreForThisLevel > highScore)
        {
            highScore = scoreForThisLevel;
        }

        scoreText.text = scoreForThisLevel.ToString();
        highScoreText.text = highScore.ToString();


        CheckWinCondition();
    }






    private void CheckWinCondition()
    {
        if(briques.Count == 0)
        {
            score = scoreForThisLevel;
            PlayerPrefs.SetInt("High Score", highScore);

            isGameWon = true;
            winPrompt.SetActive(true);
            Time.timeScale = 0f;
        }
    }







    public void DecreaseLives()
    {
        UpdateLivesText(-1);

        if(currentLives <= 0)
        {
            isGameOver = true;
            gameOverPrompt.SetActive(true);
            Time.timeScale = 0f;
        }
    }



    public void UpdateLivesText(int i)
    {
        currentLives += i;
        currentLivesText.text = currentLives.ToString();
    }




    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1f;

        isGameWon = false;
        isGameOver = false;


        if(level == 0)
        {
            Destroy(gameObject);
        }

        if(level == previousSceneIndex)
        {
            scoreForThisLevel = score = 0;
        }
        else
        {
            scoreForThisLevel = score;
        }


        scoreText.text = score.ToString();

        //currentLives = startLives;
        //currentLivesText.text = currentLives.ToString();

        gameOverPrompt.SetActive(false);
        winPrompt.SetActive(false);
    }


}
