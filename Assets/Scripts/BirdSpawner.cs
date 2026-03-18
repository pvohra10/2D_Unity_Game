using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    // In Unity, we use GameObject for items in the scene
    public GameObject birdPrefab;
    private GameObject currentBird;
    

    void Start()
    {
        SpawnBird();
    
    }

    void Update()
    {
        // Use '==' to compare values. '=' is only for setting values.
        // We also check if the bird exists before checking its health
        if (currentBird == null)
        {
            SpawnBird();
        }
    }

    void SpawnBird()
    {
        // This creates a copy of your prefab at the spawner's position
        currentBird = Instantiate(birdPrefab, new Vector3 (-20, 0, 0), Quaternion.identity);
        
    }
}