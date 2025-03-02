using UnityEngine;

public class Snowflake : MonoBehaviour
{
    public int snowflakePoints = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.Instance.AddItemScore(snowflakePoints);
            Destroy(gameObject); // Remove the snowflake after collection
        }
    }
}
