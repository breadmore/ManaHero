using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class MonsterSpawnData : MonoBehaviour
{

    public TextAsset data;
    private AllRoundData datas;


    // Dictionary to hold UpgradeSheet objects with upgrade as key
    private Dictionary<int, MonsterSpawn> MonsterSpawnDictionary;

    private void Awake()
    {
        datas = JsonUtility.FromJson<AllRoundData>(data.text);
        MonsterSpawnDictionary = new Dictionary<int, MonsterSpawn>();

        // Populating the UpgradeSheetDictionary for easy access by upgrade value
        foreach (MonsterSpawn monsterSpawn in datas.Monster_Spawn)
        {
            MonsterSpawnDictionary[monsterSpawn.Round] = monsterSpawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, int> GetAllMonsterCountsForRound(int round)
    {
        Dictionary<string, int> monsterCounts = new Dictionary<string, int>();

        if (MonsterSpawnDictionary.TryGetValue(round, out var monsterSpawn))
        {
            monsterCounts.Add("Green", monsterSpawn.Green);
            monsterCounts.Add("Blue", monsterSpawn.Blue);
            monsterCounts.Add("Red", monsterSpawn.Red);
            monsterCounts.Add("Rat", monsterSpawn.Rat);
            monsterCounts.Add("Bat", monsterSpawn.Bat);
            monsterCounts.Add("Ghost", monsterSpawn.Ghost);
            monsterCounts.Add("Crab", monsterSpawn.Crab);
            monsterCounts.Add("Golem", monsterSpawn.Golem);
            monsterCounts.Add("Skull", monsterSpawn.Skull);
        }
        else
        {
            Debug.LogError($"Round {round} data not found.");
        }

        return monsterCounts;
    }

    public Dictionary<string, int> GetTotalMonsterCounts()
    {
        Dictionary<string, int> totalMonsterCounts = new Dictionary<string, int>();

        foreach (var monsterSpawn in MonsterSpawnDictionary.Values)
        {
            AddToTotal(totalMonsterCounts, "Green", monsterSpawn.Green);
            AddToTotal(totalMonsterCounts, "Blue", monsterSpawn.Blue);
            AddToTotal(totalMonsterCounts, "Red", monsterSpawn.Red);
            AddToTotal(totalMonsterCounts, "Rat", monsterSpawn.Rat);
            AddToTotal(totalMonsterCounts, "Bat", monsterSpawn.Bat);
            AddToTotal(totalMonsterCounts, "Ghost", monsterSpawn.Ghost);
            AddToTotal(totalMonsterCounts, "Crab", monsterSpawn.Crab);
            AddToTotal(totalMonsterCounts, "Golem", monsterSpawn.Golem);
            AddToTotal(totalMonsterCounts, "Skull", monsterSpawn.Skull);
        }

        return totalMonsterCounts;
    }

    private void AddToTotal(Dictionary<string, int> totalMonsterCounts, string monsterType, int amount)
    {
        if (totalMonsterCounts.ContainsKey(monsterType))
        {
            totalMonsterCounts[monsterType] += amount;
        }
        else
        {
            totalMonsterCounts[monsterType] = amount;
        }
    }
}


[System.Serializable]
public class AllRoundData
{
    public MonsterSpawn[] Monster_Spawn;
}

[System.Serializable]
public class MonsterSpawn
{
    public int Round;
    public int Green;
    public int Blue;
    public int Red;
    public int Rat;
    public int Bat;
    public int Ghost;
    public int Crab;
    public int Golem;
    public int Skull;
}