using UnityEngine;
using System.Collections;

public class TrickController : MonoBehaviour
{
    public int trickBasePoints = 100;
    private bool isPerformingTrick = false;

    void Update()
    {
        // Detect a trick input; customize this based on your game design.
        if (Input.GetKeyDown(KeyCode.T) && !isPerformingTrick && SnowboarderPhysics.Instance.IsGrounded() != true)
        {
            StartCoroutine(PerformTrick());
        }
    }

    IEnumerator PerformTrick()
    {
        isPerformingTrick = true;

        float duration = 1f; // Rotate in 1 second
        float elapsed = 0f;
        float startRotation = transform.eulerAngles.z;
        float targetRotation = startRotation + 360f; // Full rotation

        Debug.Log("Performing trick...");

        while (elapsed < duration)
        {
            float newRotation = Mathf.Lerp(startRotation, targetRotation, elapsed / duration);
            transform.rotation = Quaternion.Euler(0, newRotation, 0); // Rotate around Z-axis
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, targetRotation, 0); // Ensure exact final rotation

        // Immediately add trick score after rotation
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddTrickScore(trickBasePoints);
        }

        Debug.Log("Trick completed! Combo: " + ScoreManager.Instance.comboCount + " Multiplier: " + ScoreManager.Instance.scoreMultiplier);

        isPerformingTrick = false;
    }
}
