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
    private float rotationAtTakeOff;
    private bool wasAirborne = false;

    private float cumulativeRotation = 0f; // Total rotation accumulated while airborne.
    private float lastRotation = 0f;       // The last recorded rotation angle.
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

        // *** Add speed-based scoring ***
        // Multiply by Time.fixedDeltaTime to avoid adding too many points each frame.
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddSpeedScore(rb.linearVelocity.magnitude * Time.fixedDeltaTime);
        }

        if (!isGrounded && wasAirborne)
        {
            float currentRotation = transform.eulerAngles.z;
            // DeltaAngle gives the shortest difference, taking wrap-around into account.
            float delta = Mathf.DeltaAngle(lastRotation, currentRotation);
            cumulativeRotation += Mathf.Abs(delta);
            lastRotation = currentRotation;
        }


        // Check if the player is upside down while grounded

        float angle = transform.eulerAngles.z;
        if (isGrounded && (angle > 120f && angle < 300f))
        {
            Debug.Log("Player crashed!");
            HandleDeath();
        }
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

            if (wasAirborne)
            {
                Debug.Log("Landed. Cumulative rotation: " + cumulativeRotation);
                // Only award bonus if the player spun at least 180�
                if (cumulativeRotation >= 180f)
                {
                    // Calculate multiplier: for every full 360� rotation add +1 multiplier
                    int multiplier = Mathf.FloorToInt(cumulativeRotation / 360f) + 1;
                    int baseTurnaroundBonus = 5000; // Adjust base bonus as needed.
                    int finalBonus = baseTurnaroundBonus * multiplier;
                    Debug.Log("Successful Turnaround! Cumulative rotation: " + cumulativeRotation +
                              " | Multiplier: " + multiplier + " | Bonus awarded: " + finalBonus);
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddTurnAroundScore(finalBonus);
                    }
                }
                wasAirborne = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            rotationAtTakeOff = transform.eulerAngles.z;
            lastRotation = rotationAtTakeOff; // Initialize the last rotation.
            cumulativeRotation = 0f;          // Reset cumulative rotation.
            wasAirborne = true;
            Debug.Log("Left ground. Rotation at takeoff: " + rotationAtTakeOff);
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
    private float AngleDifference(float a, float b)
    {
        float diff = Mathf.Abs(a - b) % 360f;
        if (diff > 180f)
        {
            diff = 360f - diff;
        }
        return diff;
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
