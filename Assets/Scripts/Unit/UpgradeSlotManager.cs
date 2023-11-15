using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSlotManager : MonoBehaviour
{
    public Transform upgradeSlotParent;
    public UpgradeSlot upgradeSlot;
    public List<UpgradeSlot> slotList = new List<UpgradeSlot>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void OnEnable()
    {
        RefreshUpgradePanel();
    }
    public void RefreshUpgradePanel()
    {

        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, UpgradeManager.instance.upgradeDataList.Count);
            slotList[i].SetSlot(UpgradeManager.instance.upgradeDataList[rand]);
            
        }
    }
}
