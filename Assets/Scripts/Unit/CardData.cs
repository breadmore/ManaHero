using UnityEngine;

[System.Serializable]
public class UnitData
{
    public Sprite unitImage;
    public Character_Unit unitPrefab;
    public UnitType name;
    public int damage;
    public float attackRange;
    public string desc;
    public int cost;
    public float followDistance;
    public float followAngle;
    public float moveSpeed = 5f;
}


[System.Serializable]
public class UpgradeData
{
    public Sprite UpgradeImage;
    public UpgradeType upgradeName;
    public int upgradeCount;
    public string desc;
}
