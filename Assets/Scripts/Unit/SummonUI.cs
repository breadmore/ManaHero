using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummonUI : MonoBehaviour
{
    public Image unitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    private int unitTypeCount =0;
    private int randUnit;
    UnitData unitData;
    UpgradeData upgradeData;
    Character_Unit character_Unit;
    private int randUpgrade;

    public void RefreshButton()
    {
        unitTypeCount = SummonManager.instance.unitDataList.Count;
        randUnit = Random.Range(0, unitTypeCount);
        unitData = SummonManager.instance.unitDataList[randUnit];
        
        randUpgrade = Random.Range(0, 3);

        unitImage.sprite = unitData.unitImage;
        nameText.text = unitData.name.ToString();

        descText.text = unitData.desc.ToString();

    }
    // Start is called before the first frame update
    void Start()
    {
        RefreshButton();
    }

    public void SelectUnit()
    {
        SummonManager.instance.audioSource.PlayOneShot(SummonManager.instance.SummonAltarSound);
        SummonManager.instance.selectUnit = unitData;

        SummonManager.instance.SummonUnit();

        SummonManager.instance.CloseSummonUI();

        LightManager.instance.GlobalLight.SetActive(true);
        //GameManager.instance.isUI = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
