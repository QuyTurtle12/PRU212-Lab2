using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab; // Assign the player prefab in the Inspector
    public Vector3 spawnPoint; // Assign a Transform for the spawn location

    private void Awake()
    {
        spawnPoint = new Vector3(-0.8f, 21f, 0f); // Set the spawn point
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is not assigned!");
            return;
        }

        // Spawn the player at the specified location
        GameObject player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);

        // Optionally, assign the "Player" tag if needed
        player.tag = "Player";
    }
}
