using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;
    public static UpgradeManager instance
    {
        get
        {

            if(_instance == null)
            {
                _instance = FindObjectOfType<UpgradeManager>();
            }
            return _instance;
        }
    }
    public GameObject UpgradePanel;

    public List<UpgradeData> upgradeDataList = new List<UpgradeData>();

    public UpgradeData selectUpgrade;
    public UpgradeType selectUpgradeType;

    public GameObject upgradeBar;
    public GameObject attackGrid;
    public GameObject speedGrid;
    public GameObject rangeGrid;
    private bool isPanelOpen = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(upgradeDataList.Count!=0)
        if (GameManager.instance.mana>=100 && isPanelOpen ==false)
        {
            UpgradePanel.SetActive(true);
            isPanelOpen = true;
        }

    }

    public void UpgradeCardSelect(Button button)
    {
        UpgradeSlot upgradeSlot = button.GetComponent<UpgradeSlot>();
        switch (upgradeSlot.upgradeData.upgradeName)
        {
            case UpgradeType.Damage:
                UpgradeAttackDamage();
                Instantiate(upgradeBar, attackGrid.transform);
                break;

            case UpgradeType.Range:
                UpgradeAttackRange();
                Instantiate(upgradeBar, rangeGrid.transform);
                break;

            case UpgradeType.Speed:
                UpgradeAttackSpeed();
                Instantiate(upgradeBar, speedGrid.transform);
                break;
        }

        GameManager.instance.ReduceMana(100);

        Time.timeScale = 1;
        
        UpgradePanel.SetActive(false);
        isPanelOpen = false;
        //GameManager.instance.isUI = false;

    }

    public void UpgradeAttackDamage()
    {
        int upgradeValue = 5;
        for (int i=0; i<SummonManager.instance.knightList.Count; i++)
        {
            SummonManager.instance.knightList[i].damage += upgradeValue;
        }

        for (int i = 0; i < SummonManager.instance.archerList.Count; i++)
        {
            SummonManager.instance.archerList[i].damage += upgradeValue;
        }

        //for (int i = 0; i < SummonManager.instance.knightList.Count; i++)
        //{
        //    SummonManager.instance.knightList[i].damage += upgradeValue;
        //}

        //for (int i = 0; i < SummonManager.instance.knightList.Count; i++)
        //{
        //    SummonManager.instance.knightList[i].damage += upgradeValue;
        //}

        upgradeDataList.Find(upgradeData => upgradeData.upgradeName == UpgradeType.Damage).upgradeCount++;

        if(upgradeDataList.Find(upgradeData => upgradeData.upgradeName == UpgradeType.Damage).upgradeCount ==5)
        upgradeDataList.RemoveAll(upgradeData => upgradeData.upgradeName == UpgradeType.Damage);

    }

    public void UpgradeAttackSpeed()
    {
        float upgradeValue = 0.2f;
        for (int i = 0; i < SummonManager.instance.knightList.Count; i++)
        {
            SummonManager.instance.knightList[i].moveSpeed -= upgradeValue;
        }

        for (int i = 0; i < SummonManager.instance.archerList.Count; i++)
        {
            SummonManager.instance.archerList[i].moveSpeed -= upgradeValue;
        }
        upgradeDataList.Find(upgradeData => upgradeData.upgradeName == UpgradeType.Speed).upgradeCount++;

        if (upgradeDataList.Find(upgradeData => upgradeData.upgradeName == UpgradeType.Speed).upgradeCount == 5)
            upgradeDataList.RemoveAll(upgradeData => upgradeData.upgradeName == UpgradeType.Speed);
    }
    public void UpgradeAttackRange()
    {
        //for (int i = 0; i < SummonManager.instance.knightList.Count; i++)
        //{
        //    SummonManager.instance.knightList[i].attackSpeed -= upgradeValue;
        //}

        //for (int i = 0; i < SummonManager.instance.archerList.Count; i++)
        //{
        //    SummonManager.instance.archerList[i].attackSpeed -= upgradeValue;
        //}
        upgradeDataList.Find(upgradeData => upgradeData.upgradeName == UpgradeType.Range).upgradeCount++;

        if (upgradeDataList.Find(upgradeData => upgradeData.upgradeName == UpgradeType.Range).upgradeCount == 5)
            upgradeDataList.RemoveAll(upgradeData => upgradeData.upgradeName == UpgradeType.Range);
    }
}
