using TMPro;
using Unity.Cinemachine;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public AudioSource shotSoundSystem;

    public AudioSource bulletTimeSound;

    public AudioSource gunCockSound;

    public float bulletTime = 100.0f;

    public Slider bulletTimeSlider;

    private bool bulletTimeActive = false;
    public float bulletTimeDrainRate = 20f; // units per second (real time)
    private float normalTimeScale = 0.75f;
    private float bulletTimeScale = 0.2f;   // how slow bullet time gets

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

        shotSoundSystem = GetComponent<AudioSource>();

        bulletTimeSlider.gameObject.SetActive(false);

        
        controlPrompt.SetActive(true);

        

        gameOver.SetActive(false);

        idleCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = bulletsLeft.ToString() + " SHOTS REMAIN";


        if (bulletTimeSlider.IsActive())
        {
         bulletTimeSlider.value = bulletTime;

        }


        if (bulletsLeft <= 0)
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            shotSoundSystem.Play();
            FireBullet();
            bulletCamera = BulletController.Instance.GetComponent<CinemachineFollow>();
            idleCamera.enabled = false;
            bulletCamera.enabled = true;
            // if shift pressed, activate bulletTime
        }
            if (Keyboard.current.shiftKey.wasPressedThisFrame && BulletController.Instance.isActiveAndEnabled)
            {
                bulletTimeSound.Play();
                InitiateBulletTime();
            }
        
    }

    public void FireBullet()
    {
        GameObject.Instantiate(BulletPrefab);
        bulletTimeSlider.gameObject.SetActive(true);

        controlPrompt.SetActive(false);

    }

    public void LoseLife(int amt)
    {
        bulletsLeft -= amt;
        controlPrompt.SetActive(true);
        idleCamera.enabled = true;
        gunCockSound.Play();


    }

    public void InitiateBulletTime()
    {

    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void EpicWin()
    {

    }

    public void ResetGame()
    {
        SceneManager.LoadScene("GameLevel_Office");
    }

    
}
