using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Spawn-Logik")]
    public float spawnInterval = 2.0f;
    public int maxAlive = 20;
    public float spawnRadius = 15f;
    public float minDistanceToPlayer = 6f;

    float timer;

    void Update()
    {
        if (!enemyPrefab || !player) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;


            int alive = CountAliveEnemies();
            if (alive >= maxAlive) return;

            Vector3 pos = GetSpawnPosition();
            var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

            var chase = enemy.GetComponent<EnemyChase>();
            if (chase) chase.target = player;
        }
    }

    Vector3 GetSpawnPosition()
    {
 
        for (int i = 0; i < 10; i++)
        {
            Vector2 r = Random.insideUnitCircle.normalized * Random.Range(minDistanceToPlayer, spawnRadius);
            Vector3 pos = player.position + new Vector3(r.x, 0f, r.y);
            pos.y = 1f; 
            return pos;
        }
        // Fallback
        return player.position + new Vector3(spawnRadius, 1f, 0f);
    }

    int CountAliveEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
