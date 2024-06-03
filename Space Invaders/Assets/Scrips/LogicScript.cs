using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int playerScore;

    public GameObject live1, live2, live3, live4, live5, live6, live7, live8, gameOverScreen;
    public GameObject[] objectsLives;

    private int lives;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        objectsLives = new GameObject[] { live1, live2, live3, live4, live5, live6, live7, live8 };
        LoadScore();
        LoadLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives >= 8)
            SetAllLivesActive(true);

        else if (lives > 0)
        {
            SetLiveActiveFromEnd(lives);
            SetLiveActiveFromStart(lives);
        }

        else
        {
            SetAllLivesActive(false);
            GameOver();
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            RestartGame();
    }

    //---------------------------------------------------------------------------------------------------------------------- 

    private void LoadLives()
    {
        lives = PlayerPrefs.GetInt("Lives", 3);
    }

    private void SaveLives(int lives)
    {
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.Save();
    }

    [ContextMenu("Add live")]
    public void AddLive()
    {
        lives++;
        SaveLives(lives);
    }

    [ContextMenu("Remove live")]
    public void Removelive()
    {
        lives--;
        SaveLives(lives);
    }

    private void SetLiveActiveFromStart(int lives)
    {
        for (int i = 0; i < lives; i++)
            objectsLives[i].SetActive(true);
    }

    private void SetLiveActiveFromEnd(int lives)
    {
        for (int i = 7; i > lives - 1; i--)
            objectsLives[i].SetActive(false);
    }

    public void SetAllLivesActive(bool isActive)
    {
        foreach (GameObject life in objectsLives)
        {
            life.SetActive(isActive);
        }
    }

    //----------------------------------------------------------------------------------------------------------------------

    private void LoadScore()
    {
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        AddScore();
    }

    private void SaveScore(int playerScore)
    {
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.Save();
    }

    public void AddScore(int score = 0)
    {
        playerScore += score;
        scoreText.text = $"Score: {playerScore}";
        SaveScore(playerScore);
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [ContextMenu("Start Again")]
    public void StartAgain()
    {
        SaveScore(playerScore);
        RestartGame();
    }

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        PlayerPrefs.DeleteKey("PlayerScore");
        PlayerPrefs.DeleteKey("Lives");

        gameOverScreen.SetActive(true);
        isGameOver = true;  
        //SaveScore(0);
        //SaveLives(3);
        //StartAgain();
    }
}
