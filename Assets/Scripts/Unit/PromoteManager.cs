using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PromoteManager : MonoBehaviour
{
    private static PromoteManager _instance;
    public static PromoteManager instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType<PromoteManager>();
            }
            return _instance;
        }
    }
    public List<PromoteData> promote_Knights = new List<PromoteData>();
    public List<PromoteData> promote_Archers = new List<PromoteData>();
    public List<PromoteData> availablePromotes = new List<PromoteData>();
    public GameObject PromoteUI;
    public GameObject PromoteObject;
    public bool canPromote = false;

    public int knightUpgrade =0;
    public int archerUpgrade =0;
    public int maxKnightUpgrade;
    public int maxArcherUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        // Add remaining promote_Knights data
        availablePromotes.AddRange(promote_Knights);

        // Add remaining promote_Archers data
        availablePromotes.AddRange(promote_Archers);
    }

    // Update is called once per frame
    void Update()
    {
        if(canPromote)
        {
            OpenPromoteUI();
            maxKnightUpgrade = CharacterManager.instance.KnightList.Count;
            maxArcherUpgrade = CharacterManager.instance.ArcherList.Count;
            if (maxKnightUpgrade == knightUpgrade)
            {
                availablePromotes.RemoveAll(promoteData => promoteData.UnitType == UnitType.Knight);
            }
            if (maxArcherUpgrade == archerUpgrade)
            {
                availablePromotes.RemoveAll(promoteData => promoteData.UnitType == UnitType.Archer);
            }
            canPromote = false;
        }
        
    }

    public void PromoteCardSelect(Button button)
    {
        PromoteSlot promoteSlot = button.GetComponent<PromoteSlot>();

        if(promoteSlot == null)
        {
            print("no");
            return;
        }
        if ((int)promoteSlot.promoteData.promoteType < 4)
        {
            PromoteUnit(CharacterManager.instance.KnightList[knightUpgrade], promoteSlot.promoteData.promoteType);
            knightUpgrade++;
            if(maxKnightUpgrade == knightUpgrade)
            {
                availablePromotes.RemoveAll(promoteData => promoteData.UnitType == UnitType.Knight);
            }
        }
        else
        {
            PromoteUnit(CharacterManager.instance.ArcherList[archerUpgrade], promoteSlot.promoteData.promoteType);
            archerUpgrade++;
            if (maxArcherUpgrade == archerUpgrade)
            {
                availablePromotes.RemoveAll(promoteData => promoteData.UnitType == UnitType.Archer);
            }
        }

        ClosePromoteUI();
    }

    public void PromoteUnit(Character_Unit character_Unit, PromoteType promoteType)
    {
        if (character_Unit.unitType == UnitType.Knight)
        {
            List<PromoteData> promoteList = promote_Knights;
            PromoteData promoteData = promoteList.Find(data => data.promoteType == promoteType);

            if (promoteData != null)
            {
                print("promote"+ promoteData.promoteType.ToString());
                ApplyPromotionImages(character_Unit, promoteData);
                GameManager.instance.UpdateUnitCount(promoteData.promoteType.ToString());
            }
        }
        else if (character_Unit.unitType == UnitType.Archer)
        {
            List<PromoteData> promoteList = promote_Archers;
            PromoteData promoteData = promoteList.Find(data => data.promoteType == promoteType);

            if (promoteData != null)
            {
                print("promote"+ promoteData.promoteType.ToString());
                ApplyPromotionImages(character_Unit, promoteData);
                GameManager.instance.UpdateUnitCount(promoteData.promoteType.ToString());
            }
        }

        
    }

    private void ApplyPromotionImages(Character_Unit character_Unit, PromoteData promoteData)
    {
        UnitBase unitBase = character_Unit.unitBase;

        unitBase.Body.GetComponent<SpriteRenderer>().sprite = promoteData.Body;
        unitBase.Head.GetComponent<SpriteRenderer>().sprite = promoteData.Head;
        unitBase.Weapon.GetComponent<SpriteRenderer>().sprite = promoteData.Weapon;
    }

    public void OpenPromoteUI()
    {
        PromoteUI.SetActive(true);
    }

    public void ClosePromoteUI()
    {
        PromoteUI.SetActive(false);
    }
}
