using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class AchievementData : MonoBehaviour
{
    public TextAsset data;
    private AllData datas;


    private Dictionary<string, MonsterCount> MonsterCountDictionary;

    private void Awake()
    {
        datas = JsonUtility.FromJson<AllData>(data.text);
        MonsterCountDictionary = new Dictionary<string, MonsterCount>();

        foreach (MonsterCount monsterCount in datas.Moster_Count)
        {
            MonsterCountDictionary[monsterCount.Monster] = monsterCount;
        }
    }

    public int GetCountForMonsterName(string monsterName)
    {
        if (MonsterCountDictionary.TryGetValue(monsterName, out var MonsterCount))
        {
            return MonsterCount.Count;
        }
        else
        {
            Debug.LogError("Monster value not found: " + monsterName);
            return 0;
        }
    }

    public void UpdateCountForMonsterName(string monsterName, int newCount)
    {
        if (MonsterCountDictionary.TryGetValue(monsterName, out var MonsterCount))
        {
            MonsterCount.Count = newCount;
        }
        else
        {
            Debug.LogError("Monster count not found: " + monsterName);
        }
    }

    public void SaveMonsterCountData()
    {
        foreach (MonsterCount monsterCount in datas.Moster_Count)
        {
            monsterCount.Count = GetCountForMonsterName(monsterCount.Monster);
        }

        string jsonData = JsonUtility.ToJson(datas, true);
        File.WriteAllText(Application.dataPath + "/Data/Monster_Count.json", jsonData);
    }
}

[System.Serializable]
public class AllData
{
    public MonsterCount[] Moster_Count;
}

[System.Serializable]
public class MonsterCount
{
    public string Monster;
    public int Count;
}