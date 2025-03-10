using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != null)
        {
            audioSource.Play(); // Phát nhạc khi game bắt đầu
            Debug.Log("Music is playing!");
        }
        else
        {
            Debug.Log("No AudioClip assigned to AudioSource.");
        }
    }

    private void Awake()
    {
        // Kiểm tra nếu đã có một MusicManager tồn tại, thì hủy đối tượng mới
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo đối tượng không bị hủy khi chuyển cảnh
            audioSource = GetComponent<AudioSource>();
        }
    }

    
}
