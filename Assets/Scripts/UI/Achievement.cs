using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Achievement : MonoBehaviour
{
    private static Achievement _instance;
    public static Achievement instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Achievement>();
            }

            return _instance;
        }

    }
    [Header("Monster")]
    public List<GameObject> medalList = new List<GameObject>();
    public AchievementData achievement;
    private int conditionOfMedal = 30;
    public GameObject monsterTooltip;
    public Image monsterImage;
    public TextMeshProUGUI monsterMedalNameText;
    public TextMeshProUGUI monsterMedalConditionText;


    public Material blackOut;
    public bool isToolTip = false;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i=0; i < medalList.Count; i++)
        {
            if (achievement.GetCountForMonsterName(medalList[i].name) < conditionOfMedal)
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
            monsterImage.sprite = medal.GetComponent<Image>().sprite;
            monsterMedalNameText.text = medal.name;
            monsterMedalConditionText.text = achievement.GetCountForMonsterName(medal.name).ToString() + "/" + conditionOfMedal.ToString();

            Vector2 tooltipPosition = new Vector2(medal.transform.position.x + 350f, medal.transform.position.y);

            if (tooltipPosition.x + monsterTooltip.GetComponent<RectTransform>().rect.width > Screen.width)
            {
                tooltipPosition.x = medal.transform.position.x - 350f;
            }

            monsterTooltip.transform.position = tooltipPosition;
            monsterTooltip.SetActive(true);
            isToolTip = true;
        }
    }

    public void CloseToolTip()
    {
        monsterTooltip.SetActive(false);
        isToolTip = false;
    }

}
