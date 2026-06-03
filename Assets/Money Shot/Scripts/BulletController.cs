using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
     private float currentThrustSpeed;
    [SerializeField] private float maxThrustSpeed;
    [SerializeField] private float minThrustSpeed;
    [SerializeField] private float thrustFactor;

    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;

    void Start()
    {
        cameraTransform = Camera.main.transform.parent;
        rb = GetComponent<Rigidbody>();
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
        // pitch will use a sine wave, cause pointing up will make us go down, while pointing down will make us speed up
        float pitchInRads = transform.eulerAngles.x * Mathf.Deg2Rad;
        float mappedPitch = Mathf.Sin(pitchInRads) * thrustFactor;
        Vector3 glidingForce = Vector3.forward * currentThrustSpeed;

        currentThrustSpeed += mappedPitch;

        currentThrustSpeed = Mathf.Clamp(currentThrustSpeed, 0, maxThrustSpeed);

        rb.AddRelativeForce(Vector3.forward * baseSpeed);
        //rb.AddRelativeForce(glidingForce);
    }
    private void ManagePlaneRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = targetRotation;
    }
}
