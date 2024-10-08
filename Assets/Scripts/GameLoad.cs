using UnityEngine;

public class GameLoad : MonoBehaviour
{
    // Reference to the asteroid prefabs
    [SerializeField]
    GameObject asteroidOnePrefab;
    [SerializeField]
    GameObject asteroidTwoPrefab;
    [SerializeField]
    GameObject asteroidThreePrefab;

    // Reference to the Asteroids parent object
    public GameObject asteroidsParent;

    // Number of asteroids to spawn
    public int numberOfAsteroids = 10;

    // Minimum and maximum distance from the player
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    // Minimum and maximum scale for the asteroids
    public float minScale = 0.5f;
    public float maxScale = 2.0f;

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

            // Randomly pick an asteroid prefab
            GameObject selectedPrefab = PickRandomAsteroidPrefab();

            // Instantiate the asteroid and set its parent
            GameObject asteroid = Instantiate(selectedPrefab, position, Quaternion.identity);
            asteroid.transform.parent = asteroidsParent.transform;

            asteroid.tag = "Selectable";

            // Randomly scale the asteroid
            float scale = Random.Range(minScale, maxScale);
            asteroid.transform.localScale = new Vector3(scale, scale, scale);

            AsteroidInfo asteroidInfo = asteroid.GetComponent<AsteroidInfo>();
            if (asteroidInfo != null)
            {
                asteroidInfo.name = GenerateRandomAsteroidName();
                asteroidInfo.type = PickRandomAsteroidType();
            }
        }
    }

    // Method to randomly pick an asteroid prefab
    GameObject PickRandomAsteroidPrefab()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return asteroidOnePrefab;
            case 1:
                return asteroidTwoPrefab;
            case 2:
                return asteroidThreePrefab;
            default:
                return asteroidOnePrefab; // Fallback, should never reach here
        }
    }

    // Method to generate a random asteroid name
    string GenerateRandomAsteroidName()
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        string randomLetters = new string(new char[] {
            letters[Random.Range(0, letters.Length)],
            letters[Random.Range(0, letters.Length)]
        });

        string randomAlphanumeric = new string(new char[] {
            alphanumeric[Random.Range(0, alphanumeric.Length)],
            alphanumeric[Random.Range(0, alphanumeric.Length)],
            alphanumeric[Random.Range(0, alphanumeric.Length)],
            alphanumeric[Random.Range(0, alphanumeric.Length)],
            alphanumeric[Random.Range(0, alphanumeric.Length)],
            alphanumeric[Random.Range(0, alphanumeric.Length)]
        });

        return $"{randomLetters}-{randomAlphanumeric}";
    }

    // Method to randomly pick an asteroid type
    string PickRandomAsteroidType()
    {
        string[] types = { "Ice", "Iron Rich", "Exotic" };
        int randomIndex = Random.Range(0, types.Length);
        return types[randomIndex];
    }
}
