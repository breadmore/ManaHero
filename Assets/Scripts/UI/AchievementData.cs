using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class AchievementData : MonoBehaviour
{
    public TextAsset data;
    private AllData datas;


    // Dictionary to hold UpgradeSheet objects with upgrade as key
    private Dictionary<string, MonsterCount> MonsterCountDictionary;

    // Start is called before the first frame update
    private void Awake()
    {
        datas = JsonUtility.FromJson<AllData>(data.text);
        MonsterCountDictionary = new Dictionary<string, MonsterCount>();

        // Populating the UpgradeSheetDictionary for easy access by upgrade value
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
            // Handle case when upgrade value is not found
            Debug.LogError("Monster value not found: " + monsterName);
            return 0; // Or return a default value as per your requirement
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