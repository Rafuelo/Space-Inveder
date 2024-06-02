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
    private int lives;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadScore();
        LoadLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives >= 8)
            ShowLive(live8);

        if (lives <= 7)
        {
            HideLive(live8);
            ShowLive(live7);
        }

        if (lives <= 6)
        {
            HideLive(live7);
            ShowLive(live6);
        }

        if (lives <= 5)
        {
            HideLive(live6);
            ShowLive(live5);
        }

        if (lives <= 4)
        {
            HideLive(live5);
            ShowLive(live4);
        }

        if (lives <= 3)
        {
            HideLive(live4);
            ShowLive(live3);
        }

        if (lives <= 2)
        {
            HideLive(live3);
            ShowLive(live2);
        }

        if (lives <= 1)
        {
            HideLive(live2);
            ShowLive(live1);
        }

        if (lives == 0)
        {
            HideLive(live1);
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

    private void HideLive(GameObject live)
    {
        live.SetActive(false);
    }

    private void ShowLive(GameObject live)
    {
        live.SetActive(true);
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
