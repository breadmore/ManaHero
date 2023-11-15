using UnityEngine;

[System.Serializable]
public enum PromoteType
{
    Katana,
    Polearm,
    Mace,
    Spear,
    Staff,
    Rod,
    Length
}

[System.Serializable]
public class UnitBase
{
    public GameObject Body;
    public GameObject Head;
    public GameObject Weapon;
}

[System.Serializable]
public class PromoteData
{
    public UnitType UnitType;
    public PromoteType promoteType;
    public Sprite Body;
    public Sprite Head;
    public Sprite Weapon;
}