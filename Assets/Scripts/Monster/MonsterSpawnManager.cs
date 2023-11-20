using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public MonsterSpawner[] monsterSpawner;
    public Monster monster;

    [Header("Monster")]
    public Monster[] monsterPrefab;
   

    private float spawnCoolTime = 10f;
    private float currentSpawnTime = 0f;

    public int round;

    private MonsterSpawnData monsterSpawn;

    private Dictionary<string, int> totalMonsterCounts;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTime = 0f;
        round = 0;
        monsterSpawn = GetComponent<MonsterSpawnData>();

       totalMonsterCounts = monsterSpawn.GetTotalMonsterCounts();

        PreSpawnMonsters();
    }

    private void Update()
    {
        if (!GameManager.instance.dead)
        {
            if (Time.timeSinceLevelLoad >= spawnCoolTime + currentSpawnTime)
            {
                if (round > 19)
                    round = 19;
                Dictionary<string, int> monsterCounts = monsterSpawn.GetAllMonsterCountsForRound(round);

                foreach (var kvp in monsterCounts)
                {
                    for (int i = 0; i < kvp.Value; i++)
                    {
                        string monsterName = kvp.Key;
                        ActivateMonster(monsterName); // Spawn monsters and set active to true
                    }
                }

                //GameManager.instance.DisplayRound(round);
                currentSpawnTime = Time.timeSinceLevelLoad;

                round++;
            }
        }
    }

    private void PreSpawnMonsters()
    {
        foreach (var kvp in totalMonsterCounts)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                SpawnMonster(kvp.Key, false); // Spawn monsters but set active to false
            }
        }
    }

    private void SpawnMonster(string monsterName, bool setActive)
    {

        Monster prefab = null;
        foreach (Monster monsterPrefab in monsterPrefab)
        {
            if (monsterPrefab.name == monsterName)
            {
                prefab = monsterPrefab;
                break;
            }
        }

        if (prefab != null)
        {
            for (int i = 0; i < monsterSpawner.Length; i++)
            {
                monsterSpawner[i].SpawnMonster(prefab);
            }

        }
        else
        {
            Debug.LogError($"Prefab not found for monster: {monsterName}");
        }
    }

    private void ActivateMonster(string monsterName)
    {
        for (int i = 0; i < monsterSpawner.Length; i++)
        {
            // Check if this is the boss monster and round is 19
            if (round != 0 && round % 5 == 1)
            {
                monsterSpawner[i].SetBossMonster(monsterName);
            }

            monsterSpawner[i].ActivateMonster(monsterName);
        }
        
    }

}
