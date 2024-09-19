using UnityEngine;

public class GameLoad : MonoBehaviour
{
    // Reference to the asteroid prefab
    public GameObject asteroidPrefab;

    // Number of asteroids to spawn
    public int numberOfAsteroids = 10;

    // Minimum and maximum distance from the player
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAsteroids();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to spawn asteroids around the player
    void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            // Generate a random distance and angle
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(0, 2 * Mathf.PI);

            // Calculate the position
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * distance,
                0, // Assuming 2D plane, adjust if needed for 3D
                Mathf.Sin(angle) * distance
            );

            // Instantiate the asteroid
            Instantiate(asteroidPrefab, position, Quaternion.identity);
        }
    }
}
