using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonManager : MonoBehaviour
{
    private static SummonManager _instance;
    public static SummonManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<SummonManager>();

            }
            return _instance;
        }
    }
    [Header("SummonAltar")]
    public GameObject SummonUI;
    public GameObject SummonAltarPrefab;
    public Vector2 gridsize;
    public int altarCount = 2;

    [Header("SummonGate")]
    public GameObject SummonGatePrefab;
    public float gateSpawnTime = 20f;

    [Header("Unit Data")]
    public List<UnitData> unitDataList =  new List<UnitData>();

    [Header("Altar")]
    public List<GameObject> altarList = new List<GameObject>();
    public GameObject selectAltar;
    public UnitData selectUnit;
    public UnitType selectUnitType;


    public List<Character_Unit> knightList = new List<Character_Unit>();
    public List<Character_Unit> archerList = new List<Character_Unit>();

    [Header("Unit Count")]
    public int knightCount = 0;
    public int archerCount = 0;

    private bool isUIOpen = false;

    private float summonAltarCoolTime = 2f;
    public AudioSource audioSource;
    public AudioClip SummonAltarSound;
    public AudioClip SummonGateSound;
    public void OpenSummonUI()
    {
        if (!isUIOpen)
        {
            SummonUI[] components = SummonUI.GetComponentsInChildren<SummonUI>();
            foreach(SummonUI component in components)
            {
                component.RefreshButton();
            }
            SummonUI.SetActive(true);
            isUIOpen = true;
        }
    }
    public void CloseSummonUI()
    {
        if (isUIOpen)
        {
            selectAltar.GetComponent<SummonAltar>().UseMana();
            SummonUI.SetActive(false);
            isUIOpen = false;
        }
    }

    public int ReturnUnitCount()
    {
        return knightList.Count + archerList.Count;
    }

    public void SummonUnit()
    {
        UnitData summonUnitData = selectUnit;
        SetUnitAngle(summonUnitData);
        Character_Unit character_Unit = summonUnitData.unitPrefab;
        character_Unit.damage = summonUnitData.damage;
        character_Unit.attackRange = summonUnitData.attackRange;
        character_Unit.followDistance = summonUnitData.followDistance;
        character_Unit.moveSpeed = summonUnitData.moveSpeed;
        Vector2 pos = selectAltar.transform.position;
        Character_Unit summonUnit = Instantiate(character_Unit, pos, Quaternion.identity);

        SetUnitList(summonUnit);
        SetUnitCount(summonUnit);

        CloseSummonUI();
    }

    public void SummonRandUnit(Vector2 gatePos)
    {
        int rand = Random.Range(0, unitDataList.Count);
        UnitData summonUnitData = unitDataList[rand];
        SetUnitAngle(summonUnitData);
        Character_Unit character_Unit = summonUnitData.unitPrefab;
        character_Unit.damage = summonUnitData.damage;
        character_Unit.attackRange = summonUnitData.attackRange;
        character_Unit.followDistance = summonUnitData.followDistance;
        character_Unit.moveSpeed = summonUnitData.moveSpeed;

        Vector2 pos = gatePos;
        Character_Unit summonUnit = Instantiate(character_Unit, pos, Quaternion.identity);

        SetUnitList(summonUnit);
        SetUnitCount(summonUnit);
    }

    private void SetUnitAngle(UnitData unitData)
    {
        Character_Unit character_Unit = unitData.unitPrefab;
        switch (unitData.name)
        {
            case (UnitType)0:
                knightCount++;
                selectUnitType = UnitType.Knight;
                break;
            case (UnitType)1:
                archerCount++;
                selectUnitType = UnitType.Archer;
                break;
        }

    }

    private void SetUnitList(Character_Unit character_Unit)
    {
        switch (selectUnitType)
        {
            case (UnitType)0:
                knightList.Add(character_Unit);
                break;
            case (UnitType)1:
                archerList.Add(character_Unit);
                break;
        }

    }

    private void SetUnitCount(Character_Unit character_Unit)
    {
        switch (selectUnitType)
        {
            case (UnitType)0:
                CharacterManager.instance.SummonKnightUnit(character_Unit);
                break;
            case (UnitType)1:
                CharacterManager.instance.SummonArcherUnit(character_Unit);

                break;
        }

    }

    private IEnumerator SpawnCoroutain()
    {
        while (altarCount > 0)
        {
            // Select two random quadrants
            List<Vector2> quadrants = GetRandomQuadrants(2);

            foreach (Vector2 quadrant in quadrants)
            {
                Vector2 randomPosition = GetRandomPositionInQuadrant(quadrant);

                GameObject spawnedAltar = Instantiate(SummonAltarPrefab, randomPosition, Quaternion.identity);
                altarList.Add(spawnedAltar);
                altarCount--;
            }

            yield return new WaitForSeconds(summonAltarCoolTime);
        }
    }

    private List<Vector2> GetRandomQuadrants(int count)
    {
        List<Vector2> availableQuadrants = new List<Vector2>
    {
        new Vector2(-1, 1),  // Top-left quadrant
        new Vector2(1, 1),   // Top-right quadrant
        new Vector2(-1, -1), // Bottom-left quadrant
        new Vector2(1, -1)   // Bottom-right quadrant
    };

        List<Vector2> selectedQuadrants = new List<Vector2>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableQuadrants.Count);
            selectedQuadrants.Add(availableQuadrants[randomIndex]);
            availableQuadrants.RemoveAt(randomIndex);
        }

        return selectedQuadrants;
    }

    private Vector2 GetRandomPositionInQuadrant(Vector2 quadrant)
    {
        float xOffset = quadrant.x * gridsize.x / 4f;
        float yOffset = quadrant.y * gridsize.y / 4f;

        float randomX = Random.Range(transform.position.x - gridsize.x / 8f + xOffset, transform.position.x + gridsize.x / 8f + xOffset);
        float randomY = Random.Range(transform.position.y - gridsize.y / 8f + yOffset, transform.position.y + gridsize.y / 8f + yOffset);

        return new Vector2(randomX, randomY);
    }

    private IEnumerator SpawnGate()
    {
        while (ReturnUnitCount() < 10)
        {
            Vector2 randomPosition = GetRandomPositionInGrid();

            GameObject spawnGate = Instantiate(SummonGatePrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(gateSpawnTime);
        }
    }

    private Vector2 GetRandomPositionInGrid()
    {
        float randomX = Random.Range(transform.position.x - gridsize.x / 2f, transform.position.x + gridsize.x / 2f);
        float randomY = Random.Range(transform.position.y - gridsize.y / 2f, transform.position.y + gridsize.y / 2f);

        return new Vector2(randomX, randomY);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutain());
        StartCoroutine(SpawnGate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
