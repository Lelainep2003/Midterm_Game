using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject SpawnedObject; // Assign hazard prefab in Inspector
    public Vector2 minSpawnPosition = new Vector2(-7f, -4f);
    public Vector2 maxSpawnPosition = new Vector2(7f, 4f);

    private int currentThreshold = 0;
    private int totalHazards = 1;

    void Start()
    {
        // One hazard already exists in the scene
    }

    public void CheckAndSpawn(int playerScore)
    {
        int[] thresholds = { 5, 10, 15 };
        int[] increases = { 2, 3, 5 };

        while (currentThreshold < thresholds.Length && playerScore >= thresholds[currentThreshold])
        {
            Spawn(increases[currentThreshold]);
            currentThreshold++;
        }

        if (playerScore > 20 && (playerScore - 20) % 5 == 0)
        {
            int expected = 14 + ((playerScore - 20) / 5) * 3;
            if (totalHazards < expected)
            {
                Spawn(3);
            }
        }
    }

    private void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            float y = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
            Instantiate(SpawnedObject, new Vector3(x, y, 0f), Quaternion.identity);
            totalHazards++;
        }
    }

    public void ResetSpawner()
    {
        currentThreshold = 0;
        totalHazards = 1;

        GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
        foreach (GameObject h in hazards)
        {
            Destroy(h);
        }

        Spawn(1); // Respawn the starting hazard
    }
}