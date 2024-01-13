using UnityEngine;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab; // Drag your Boid prefab here in the Inspector
    public int boidCount = 2;
    public float spawnRadius = 5f;

    void Start()
    {
        for (int i = 0; i < boidCount; i++)
    {
        Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;
        GameObject boid = Instantiate(boidPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Boid instantiated at: " + spawnPosition);
    }
    }
}
