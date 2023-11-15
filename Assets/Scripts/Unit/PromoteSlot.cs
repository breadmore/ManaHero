using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PromoteSlot : MonoBehaviour
{

    public TextMeshProUGUI promoteText;
    public Image promoteImage;
    public PromoteData promoteData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlot(PromoteData _promoteData)
    {
        if (_promoteData != null)
        {
            promoteData = _promoteData;
            promoteText.text = promoteData.promoteType.ToString();
            promoteImage.sprite = promoteData.Weapon;
        }
        else
        {
            print("No data");
        }
    }

    public void ClearSlot()
    {
        promoteText.text = "Max Upgrade";
        promoteImage.sprite = null;
        Destroy(GetComponent<PromoteSlot>());
    }
}
