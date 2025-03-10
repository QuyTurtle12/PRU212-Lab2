using UnityEngine;
using UnityEngine.Audio;

public class DestroyBoost : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false; 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}