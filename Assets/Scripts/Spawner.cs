using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes prefab;
    public float spawnRateMin = 1f;
    public float spawnRateMax = 3f;
    public float minHeight = -1f;
    public float maxHeight = 1f;
    public float verticalGap = 3f;

    private void OnEnable()
    {
        ScheduleNextSpawn();
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        // Instantiate the prefab and adjust position
        Pipes pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

        // Set a random gap for the spawned object
        float randomGap = Random.Range(2, 4);
        pipes.gap = randomGap;

        // Schedule the next spawn
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        // Cancel any existing invokes
        CancelInvoke(nameof(Spawn));

        // Schedule the next spawn with a new random interval
        float nextSpawnTime = Random.Range(spawnRateMin, spawnRateMax);
        Invoke(nameof(Spawn), nextSpawnTime);
    }
}
