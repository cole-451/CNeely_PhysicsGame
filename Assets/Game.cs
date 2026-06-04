using TMPro;
using Unity.Cinemachine;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Game : Singleton<Game>
{
    public int bulletsLeft;
    public TMP_Text bulletText;

    public GameObject controlPrompt;
    public GameObject gameOver;

    [SerializeField]public GameObject bulletSpawnLocation;

    [SerializeField]public GameObject BulletPrefab;

    private CinemachineFollow bulletCamera;

   [SerializeField] private Camera idleCamera;

    public AudioSource soundSystem;

    public enum GameState
    {
        StartShot,
        LiveShot,
        GameOver
    }

    public GameState state;



    private bool bulletFired = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsLeft = 3;
        Time.timeScale = 0.75f;

        soundSystem = GetComponent<AudioSource>();

        
        controlPrompt.SetActive(true);

        gameOver.SetActive(false);

        idleCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = bulletsLeft.ToString() + " SHOTS REMAIN";


        if (bulletsLeft <= 0)
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            soundSystem.Play();
            FireBullet();
            bulletCamera = BulletController.Instance.GetComponent<CinemachineFollow>();
            idleCamera.enabled = false;
            bulletCamera.enabled = true;
        }
        
    }

    public void FireBullet()
    {
        GameObject.Instantiate(BulletPrefab);
        controlPrompt.SetActive(false);

    }

    public void LoseLife(int amt)
    {
        bulletsLeft -= amt;
        controlPrompt.SetActive(true);
        idleCamera.enabled = true;


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
