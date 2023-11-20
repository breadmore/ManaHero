using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class UnitAchievementData : MonoBehaviour
{
    public TextAsset data;
    private UnitAllData datas;


    private Dictionary<string, UnitCount> UnitCountDictionary;

    private void Awake()
    {
        datas = JsonUtility.FromJson<UnitAllData>(data.text);
        UnitCountDictionary = new Dictionary<string, UnitCount>();

       foreach (UnitCount unitCount in datas.Unit_Count)
        {
            UnitCountDictionary[unitCount.Unit] = unitCount;
        }
    }

    public int GetCountForUnitName(string unitName)
    {
        if (UnitCountDictionary.TryGetValue(unitName, out var UnitCount))
        {
            return UnitCount.Count;
        }
        else
        {
            // Handle case when upgrade value is not found
            Debug.LogError("Unit value not found: " + unitName);
            return 0;
        }
    }

    public void UpdateCountForUnitName(string unitName, int newCount)
    {
        if (UnitCountDictionary.TryGetValue(unitName, out var UnitCount))
        {
            UnitCount.Count = newCount;

        }
        else
        {
            Debug.LogError("Unit count not found: " + unitName);
        }
    }

    public void SaveUnitCountData()
    {
        foreach (UnitCount unitCount in datas.Unit_Count)
        {
            unitCount.Count = GetCountForUnitName(unitCount.Unit);
        }

        string jsonData = JsonUtility.ToJson(datas, true);
        File.WriteAllText(Application.dataPath + "/Data/Unit_Count.json", jsonData);
    }
}


[System.Serializable]
public class UnitAllData
{
    public UnitCount[] Unit_Count;
}

[System.Serializable]
public class UnitCount
{
    public string Unit;
    public int Count;
}