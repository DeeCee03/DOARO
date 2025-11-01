using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnRadius = 15f;
    public float minDistanceToPlayer = 6f;

    [Header("Legacy Auto-Spawn (off)")]
    public bool autoSpawn = false;
    public float spawnInterval = 2f;
    public int maxAlive = 20;

    private float timer;

    void Update()
    {

        if (!autoSpawn) return;
        if (!enemyPrefab || !player) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            if (CountAliveEnemies() >= maxAlive) return;
            SpawnEnemyInstant();
        }
    }

    public void SpawnEnemyInstant()
    {
        if (!enemyPrefab || !player) return;

        Vector3 pos = GetSpawnPosition();
        var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        var chase = enemy.GetComponent<EnemyChase>();
        if (chase) chase.target = player;
    }

    Vector3 GetSpawnPosition()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector2 r = Random.insideUnitCircle.normalized * Random.Range(minDistanceToPlayer, spawnRadius);
            Vector3 pos = player.position + new Vector3(r.x, 0f, r.y);
            pos.y = 1f;

            int mask = ~LayerMask.GetMask("Ground", "Player");
            if (!Physics.CheckSphere(pos, 0.6f, mask))
                return pos;
        }

        return player.position + new Vector3(spawnRadius, 1f, 0f);
    }

    public int CountAliveEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
