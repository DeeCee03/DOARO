using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public EnemySpawner spawner;

    [Header("Wave Settings")]
    public int enemiesPerWave = 20;
    public float delayBetweenSpawns = 0.2f;
    public float delayBetweenWaves = 3f;

    private int currentWave = 0;
    private GUIStyle style;

    IEnumerator Start()
    {
       
        if (spawner != null) spawner.autoSpawn = false;

        // UI Style
        style = new GUIStyle
        {
            fontSize = 32,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white }
        };

  
        while (true)
        {
       
            currentWave++;

           
            for (int i = 0; i < enemiesPerWave; i++)
            {
                spawner.SpawnEnemyInstant();
                yield return new WaitForSeconds(delayBetweenSpawns);
            }

         
            while (spawner.CountAliveEnemies() > 0)
                yield return null;

         
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 60, 300, 50), $"Round: {currentWave}", style);
    }
}
