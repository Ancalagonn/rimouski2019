using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSceneManager : MonoBehaviour
{
    public GameObject[] LootSpawnPoints;
    public GameObject[] EnemySpawnPoints;

    public GameObject LootPrefab;
    public GameObject EnemyPrefab;

    private int lootSpawnId1 = -1;
    private int lootSpawnId2 = -1;

    private int enemySpawnId1 = -1;
    private int enemySpawnId2 = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        if (LootSpawnPoints == null || EnemySpawnPoints == null) {
            Debug.Log("No loot spawnpoints or no enemy spawn points in boatscene");
            return;
        }

        // Get Random Ids
        while (lootSpawnId1 == lootSpawnId2) {
            lootSpawnId1 = Random.Range(0, this.LootSpawnPoints.Length -1);
            lootSpawnId2 = Random.Range(0, this.LootSpawnPoints.Length -1);
        }
        while (enemySpawnId1 == enemySpawnId2) {
            enemySpawnId1 = Random.Range(0, this.EnemySpawnPoints.Length -1);
            enemySpawnId2 = Random.Range(0, this.EnemySpawnPoints.Length -1);
        }

        // Spawn loots and enemy
        Instantiate(LootPrefab, 
                    this.LootSpawnPoints[lootSpawnId1].transform.position, 
                    this.LootSpawnPoints[lootSpawnId1].transform.rotation);
        Instantiate(LootPrefab, 
                    this.LootSpawnPoints[lootSpawnId2].transform.position, 
                    this.LootSpawnPoints[lootSpawnId2].transform.rotation);

        Instantiate(EnemyPrefab, 
                    this.LootSpawnPoints[enemySpawnId1].transform.position, 
                    this.LootSpawnPoints[enemySpawnId1].transform.rotation);
        Instantiate(EnemyPrefab, 
                    this.LootSpawnPoints[enemySpawnId2].transform.position, 
                    this.LootSpawnPoints[enemySpawnId2].transform.rotation);

    }
}
