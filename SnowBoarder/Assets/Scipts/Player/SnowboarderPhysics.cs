using System.Collections;
using UnityEngine;

public class SnowboarderPhysics: MonoBehaviour
{
    public static SnowboarderPhysics Instance { get; private set; }
    public float turnSpeed = 40f; // Adjust turning sensitivity
    public float maxSpeed = 100f; // Max speed allowed
    private Rigidbody2D rb;
    private float moveInput;
    private float baseSpeed = 5f;
    private bool isGrounded = false;
    private bool isBoosting = false;
    public float boostForce = 20f;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        rb.sharedMaterial = Resources.Load<PhysicsMaterial2D>("SnowPhysics"); // Assign Physics Material
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal"); // Get player input

        // Check if the player is upside down while grounded

        float angle = transform.eulerAngles.z;
        if (isGrounded && (angle > 100f && angle < 280f))
        {
            Debug.Log("Player crashed!");
            HandleDeath();
        }

        // Apply temporary speed boost when spacebar is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = true;
            rb.AddForce(transform.right * boostForce, ForceMode2D.Impulse);
            StartCoroutine(ResetBoost());
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * baseSpeed, ForceMode2D.Force);

        // Apply turning
        rb.AddTorque(-moveInput * turnSpeed);

        // Limit speed
        Vector2 clampedVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        rb.linearVelocity = clampedVelocity;
    }
    private void HandleDeath()
    {
        float destroyAfter = 1f; // Time before destroying the player
        rb.linearVelocity = Vector2.zero; // Stop movement
        StartCoroutine(DelayedDestroy(destroyAfter)); // Destroy the player
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool IsBoosting()
    {
        return isBoosting;
    }

    private IEnumerator ResetBoost()
    {
        yield return new WaitForSeconds(0.5f); // Boost duration
        isBoosting = false;
    }
}
