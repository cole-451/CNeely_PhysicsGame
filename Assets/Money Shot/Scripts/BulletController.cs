using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : Singleton<BulletController>
{
    [SerializeField] private float baseSpeed;
     private float currentThrustSpeed;
    [SerializeField] private float maxThrustSpeed;
    [SerializeField] private float minThrustSpeed;
    [SerializeField] private float thrustFactor;
    [SerializeField] public CinemachineFollow bulletCam;

    

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletCam = GetComponent<CinemachineFollow>();
       
    }

    private void FixedUpdate()
    {
        GlidingMovement();
        if (Keyboard.current.wKey.isPressed == true)
        {
            rb.AddForce(Vector3.up * baseSpeed);
        }
        else if (Keyboard.current.aKey.isPressed == true)
        {
            rb.AddForce(Vector3.left * baseSpeed);
        }
        else if (Keyboard.current.sKey.isPressed == true)
        {
            rb.AddForce(Vector3.right * baseSpeed);
        }
        else if (Keyboard.current.dKey.isPressed == true)
        {
            rb.AddForce(Vector3.down * baseSpeed);
        }
    }

    void Update()
    {
       
    }

    private void GlidingMovement()
    {
       

        rb.AddRelativeForce(Vector3.forward * baseSpeed);
        //rb.AddRelativeForce(glidingForce);
    }
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "bullet")
        {
            Debug.Log($"I am getting killed on {collision.gameObject.name}");
            Game.Instance.EndBulletTime();
            Destroy(gameObject);
            if(collision.gameObject.tag != "KILLTHISGUY")
            {
            Game.Instance.LoseLife(1);
            }
            else
            {
                Game.Instance.EpicWin();
                // you win!!!
            }
            
        }
    }
}
