using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteSlotManager : MonoBehaviour
{
    public Transform promoteSlotParent;
    public PromoteSlot promoteSlot;
    public List<PromoteSlot> slotList = new List<PromoteSlot>();
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
        RefreshPromotePanel();
    }
    public void RefreshPromotePanel()
    {

        for (int i = 0; i < 2; i++)
        {
            if (PromoteManager.instance.availablePromotes.Count > 0)
            {
                int rand = Random.Range(0, PromoteManager.instance.availablePromotes.Count);
                slotList[i].SetSlot(PromoteManager.instance.availablePromotes[rand]);
            }
            else
            {
                slotList[i].ClearSlot();
                return;
            }
        }
    }
}
