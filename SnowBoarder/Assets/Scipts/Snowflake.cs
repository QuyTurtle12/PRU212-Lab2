using UnityEngine;

public class Snowflake : MonoBehaviour
{
    public int snowflakePoints = 50;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    private void Start()
    {
        audioSource  = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddItemScore(snowflakePoints);
            }

            if (audioSource != null)
            {   
                audioSource.Play();
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            if (collider2D != null)
            {
                collider2D.enabled = false;
            }
                Destroy(gameObject, audioSource.clip.length);

        }
    }
}
    