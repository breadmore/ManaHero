using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedalSlot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Implement the IPointerEnterHandler interface method
    public void OnPointerEnter(PointerEventData eventData)
    {
        Achievement.instance.OpenToolTip(gameObject);
    }

    // Implement the IPointerExitHandler interface method
    public void OnPointerExit(PointerEventData eventData)
    {
        Achievement.instance.CloseToolTip();
    }
}
