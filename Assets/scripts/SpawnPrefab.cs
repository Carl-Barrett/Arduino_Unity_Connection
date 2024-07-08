using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    public float spacing = 1.0f; // The distance between each spawned prefab
    public float minX = -2.5f; // The minimum x position of the spawn area
    public float maxX = 2.5f; // The maximum x position of the spawn area
    public float minY = 0.0f; // The minimum y position of the spawn area
    public float maxY = 5.0f; // The maximum y position of the spawn area
    public float minZ = -2.5f; // The minimum z position of the spawn area
    public float maxZ = 2.5f; // The maximum z position of the spawn area
    public int numberofcubes = 10;

    void Start()
    {
        // Spawn 10 instances of the prefab
        for (int i = 0; i < numberofcubes; i++)
        {
            // Generate a random position within the specified range
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 position = transform.position + new Vector3(x, y, z);

            // Spawn the prefab at the randomized position
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}


