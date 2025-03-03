using UnityEngine;
using System.Collections;

public class TrickController : MonoBehaviour
{
    public int trickBasePoints = 100;
    private bool isPerformingTrick = false;

    void Update()
    {
        // Detect a trick input; customize this based on your game design.
        if (Input.GetKeyDown(KeyCode.T) && !isPerformingTrick)
        {
            StartCoroutine(PerformTrick());
        }
    }

    IEnumerator PerformTrick()
    {
        isPerformingTrick = true;

        // Here you might play a trick animation or trigger physics adjustments.
        Debug.Log("Performing trick...");
        yield return new WaitForSeconds(1.0f);  // simulate trick duration

        // On successful trick completion, add trick score.
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddTrickScore(trickBasePoints);
        }
        Debug.Log("Trick completed and points awarded!");

        isPerformingTrick = false;
    }
}
