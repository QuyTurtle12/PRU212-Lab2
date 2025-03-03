using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineCamera cinemachineCam;

    private void Update()
    {
        cinemachineCam = GetComponentInChildren<CinemachineCamera>();

        GameObject player = GameObject.FindWithTag("Player"); // Find player in the scene
        if (SnowboarderPhysics.Instance == null || cinemachineCam.Follow == player.transform) return;

        if (player != null)
        {
            cinemachineCam.Follow = player.transform; // Set the camera to follow the player
            Debug.Log("Camera is now following the player!");
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }
}
