using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public float score;
    public TMP_Text scoreText;

    public GameObject gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0.00f;
        Time.timeScale = 0.75f;

        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString("F2");
    }

    public void AddScore(int amt)
    {
        score += amt;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("GameLevel_Office");
    }

    
}
