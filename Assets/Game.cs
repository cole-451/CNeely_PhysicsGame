using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public float money;
    public TMP_Text moneyText;

    public GameObject gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        money = 0.00f;
        Time.timeScale = 1;

        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + money.ToString("F2");
    }

    public void AddMoney(int amt)
    {
        money += amt;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    
}
