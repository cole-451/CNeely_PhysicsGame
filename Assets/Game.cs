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

    public AudioSource bulletTimeExitSound;

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
        Time.timeScale = normalTimeScale;

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

        // Bullet time drain
        if (bulletTimeActive)
        {
            bulletTime -= bulletTimeDrainRate * Time.unscaledDeltaTime;
            bulletTime = Mathf.Max(bulletTime, 0f); // clamp to 0

            // End bullet time if drained or shift released
            if (bulletTime <= 0 || !Keyboard.current.shiftKey.isPressed)
            {
                EndBulletTime();
            }
        }


        if (bulletsLeft <= 0)
        {
            GameOver();
        }

        if (Keyboard.current.shiftKey.wasPressedThisFrame && BulletController.Instance.isActiveAndEnabled)
        {
            bulletTimeSound.Play();
            InitiateBulletTime();
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
          
        
    }

    public void FireBullet()
    {
        GameObject.Instantiate(BulletPrefab);
        bulletTimeSlider.gameObject.SetActive(true);
        bulletTime = 100f;

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
        if (bulletTime <= 0) return; // don't activate if empty

        bulletTimeActive = true;
        Time.timeScale = bulletTimeScale;
    }
    public void EndBulletTime()
    {
        bulletTimeActive = false;
        Time.timeScale = normalTimeScale;
        bulletTimeExitSound.Play();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("YouSuck");
    }

    public void EpicWin()
    {
        SceneManager.LoadScene("YouWin");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("GameLevel_Office");
    }

    
}
