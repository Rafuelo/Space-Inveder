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
    public GameObject[] gameObjectsLives;

    private int lives;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObjectsLives = new GameObject[] { live1, live2, live3, live4, live5, live6, live7, live8 };
        LoadScore();
        LoadLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives >= 8)
            SetLiveActive(live8, true);

        if (lives == 7)
        {
            SetLiveActive(live8, false);
            SetLiveActive(live7, true);
        }

        if (lives == 6)
        {
            SetLiveActive(live7, false);
            SetLiveActive(live6, true);
        }

        if (lives == 5)
        {
            SetLiveActive(live6, false);
            SetLiveActive(live5, true);
        }

        if (lives == 4)
        {
            SetLiveActive(live5, false);
            SetLiveActive(live4, true);
        }

        if (lives == 3)
        {
            SetLiveActive(live4, false);
            SetLiveActive(live3, true);
        }

        if (lives == 2)
        {
            SetLiveActive(live3, false);
            SetLiveActive(live2, true);
        }

        if (lives == 1)
        {
            SetLiveActive(live2, false);
            SetLiveActive(live1, true);
        }

        if (lives == 0)
        {
            SetLiveActive(live1, false);
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

    private void SetLiveActive(GameObject live, bool isActive)
    {
        live.SetActive(isActive);
    }

    public void SetAllLivesActive(bool isActive)
    {
        foreach (GameObject life in gameObjectsLives)
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
