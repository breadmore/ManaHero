using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UnitAchievement : MonoBehaviour
{

    private static UnitAchievement _instance;
    public static UnitAchievement instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UnitAchievement>();
            }

            return _instance;
        }

    }

    [Header("Unit")]
    public List<GameObject> medalList = new List<GameObject>();
    public UnitAchievementData achievement;
    private int conditionOfMedal = 10;
    public GameObject unitTooltip;
    public Image unitImage;
    public TextMeshProUGUI unitMedalNameText;
    public TextMeshProUGUI unitMedalConditionText;


    public Material blackOut;
    public bool isToolTip = false;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < medalList.Count; i++)
        {
            if (achievement.GetCountForUnitName(medalList[i].name) < conditionOfMedal)
            {
                medalList[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenToolTip(GameObject medal)
    {
        if (!isToolTip)
        {
            unitImage.sprite = medal.GetComponent<Image>().sprite;
            unitMedalNameText.text = medal.name;
            unitMedalConditionText.text = achievement.GetCountForUnitName(medal.name).ToString() + "/" + conditionOfMedal.ToString();

            Vector2 tooltipPosition = new Vector2(medal.transform.position.x + 350f, medal.transform.position.y);

            if (tooltipPosition.x + unitTooltip.GetComponent<RectTransform>().rect.width > Screen.width)
            {
                tooltipPosition.x = medal.transform.position.x - 350f;
            }

            unitTooltip.transform.position = tooltipPosition;
            unitTooltip.SetActive(true);
            isToolTip = true;
        }
    }

    public void CloseToolTip()
    {
        unitTooltip.SetActive(false);
        isToolTip = false;
    }
}
