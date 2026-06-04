using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : Singleton<Game>
{
    public int bulletsLeft;
    public TMP_Text bulletText;

    public GameObject controlPrompt;
    public GameObject gameOver;

    [SerializeField]public GameObject bulletSpawnLocation;

    [SerializeField]public GameObject BulletPrefab;



    private bool bulletFired = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsLeft = 3;
        Time.timeScale = 0.75f;

        controlPrompt.SetActive(true);

        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = bulletsLeft.ToString("F2") + " SHOTS REMAIN";

        if(bulletsLeft <= 0)
        {
            GameOver();
        }
    }

    public void FireBullet()
    {
        // perhaps find a way to switch to the bullet's cinemachine camera?
        GameObject.Instantiate(BulletPrefab);
        controlPrompt.SetActive(false);

    }

    public void LoseLife(int amt)
    {
        bulletsLeft -= amt;
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
