using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UpgradeSlot : MonoBehaviour
{
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI descText;
    public Image upgradeImage;
    public UpgradeData upgradeData;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlot(UpgradeData _upgradeData)
    {
        if (_upgradeData != null)
        {
            upgradeData = _upgradeData;
            upgradeText.text = upgradeData.upgradeName.ToString();
            descText.text = upgradeData.desc.ToString();
            upgradeImage.sprite = upgradeData.UpgradeImage;
        }
        else
        {
            print("No data");
        }
    }

    public void ClearSlot()
    {
        upgradeText = null;
        upgradeImage = null;
        descText = null;
    }
}
