﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;         //A reference to our game control script so we can access it statically.
    public Text scoreText;                      //A reference to the UI text component that displays the player's score.
    public Text highText;
    public GameObject gameOvertext;             //A reference to the object that displays the text which appears when the player dies.

    public int score = 0;                       //The player's score.
    int highScore;
    public bool gameOver = false;               //Is the game over?
    public float scrollSpeed = -1.5f;

    public bool slowDown = false;
    public bool hasStarted = false;

    void Awake()
    {

        highScore = PlayerPrefs.GetInt("High");
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    void Update()
    {
        //If the game is over and the player has pressed some input...
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (slowDown)
        {
            if (Time.timeScale <= 1)
            {
                Time.timeScale += Time.deltaTime/2.5f;
            }
			else
			{
                slowDown = false;
			}
            
        }
    }

    public void BirdScored()
    {
        //The bird can't score if the game is over.
        if (gameOver)
            return;
        //If the game is not over, increase the score...
        score++;
        //...and adjust the score text.
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        //Activate the game over text.
        gameOvertext.SetActive(true);
        //Set the game to be over.
        gameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("High", highScore);
        }

        highText.text = "Best: " + highScore;
    }

    public void SlowDownGamePlay()//Slow Down Gameplay after bird gets hit
    {
        slowDown = true;
        Time.timeScale = 0.4f;

    }


}
