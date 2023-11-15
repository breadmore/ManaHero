using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    public static CharacterManager instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = FindObjectOfType<CharacterManager>();

            }
            return _instance;
        }
    }

    public Character_Player Player;

    [Header("Summon Unit")]
    public Transform PlayerCenter;
    public DeployPosition DeployPositionPrefab;
    public List<Character_Unit> KnightList = new List<Character_Unit>();
    public List<Character_Unit> ArcherList = new List<Character_Unit>();

    private void SetDeploy(Character_Unit unit)
    {
        DeployPosition position = Instantiate(DeployPositionPrefab, PlayerCenter.transform);
        unit.DeployPosition = position;
    }

    private void SetDeployPosition(Character_Unit unit, DeployPosition dep, int unitNumber,int unitCount, float rand)
    {
        dep.angle = (360 / unitCount) * unitNumber + rand;
        
        dep.distance = unit.followDistance;
        dep.PlayerCenter = PlayerCenter;
        dep.UpdatePosition();
    }
    public void SummonKnightUnit(Character_Unit unit)
    {
        SetDeploy(unit);
        KnightList.Add(unit);
        float rand = Random.Range(0, 360);
        for(int i=0; i<KnightList.Count; i++)
        {
            SetDeployPosition(unit, KnightList[i].DeployPosition, i, SummonManager.instance.knightCount, rand);
        }
    }
    public void SummonArcherUnit(Character_Unit unit)
    {
        SetDeploy(unit);
        ArcherList.Add(unit);
        float rand = Random.Range(0, 360);
        for (int i = 0; i < ArcherList.Count; i++)
        {
            SetDeployPosition(unit, ArcherList[i].DeployPosition, i, SummonManager.instance.archerCount,rand);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
