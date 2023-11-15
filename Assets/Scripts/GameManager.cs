using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }

    }
    public bool dead = false;
    public int score;
    [Header("Mana")]
    public Slider manaSlider;
    public TextMeshProUGUI manaText;
    public int mana;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    [Header("Round")]
    public TextMeshProUGUI roundText;
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f;
    public float visibleDuration = 2.0f;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    private float startTime;
    private int integerTimer = 0;
    private int minutes; // 분 계산
    private int seconds; // 초 계산

    public bool isUI = false;

    public GameObject settingPanel;
    private bool isSetting = false;

    public AchievementData achievement;
    public UnitAchievementData unitAchievement;

    private AudioSource audioSource;
    public AudioClip manaClip;
    public AudioClip upgradeClip;


    private bool isGameOver = false;
    // Start is called before the first frame update
    void Awake()
    {
       
        audioSource = GetComponent<AudioSource>();
        //mana = 0;
       
    }

    private void OnEnable()
    {
        isUI = false;
        dead = false;
        manaSlider.maxValue = 100;
        manaSlider.value = mana;
        manaText.text = manaSlider.value + "/" + manaSlider.maxValue;
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            startTime += Time.deltaTime;
            integerTimer = Mathf.FloorToInt(startTime);

            minutes = integerTimer / 60; // 분 계산
            seconds = integerTimer % 60; // 초 계산

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            Time.timeScale = 1f;
        }

        Time.timeScale = isUI ? 0f : 1f;

    }

    public void AddMana(int add)
    {
        mana += add;
        if (mana > 100)
        {
            mana = 100;
            audioSource.PlayOneShot(upgradeClip);
        }
        RewriteManaBar();
        audioSource.PlayOneShot(manaClip);
    }

    public void ReduceMana(int reduce)
    {
        mana -= reduce;
        if (mana < 0)
        {
            mana = 0;
        }
        RewriteManaBar();

    }

    private void RewriteManaBar()
    {
        manaSlider.value = mana;
        manaText.text = manaSlider.value + "/" + manaSlider.maxValue;
    }


    public void DisplayRound(int round)
    {
        StartCoroutine(FadeInText(round));
    }

    private IEnumerator FadeInText(int round)
    {
        roundText.text = "ROUND : " + round.ToString();
        roundText.gameObject.SetActive(true);
        
        float currentTime = 0;
        while(currentTime < fadeInDuration)
        {
            float alpha = currentTime / fadeInDuration;
            roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        currentTime = 0;
        while(currentTime < fadeOutDuration)
        {
            float alpha = 1 - (currentTime / fadeOutDuration);
            roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        roundText.gameObject.SetActive(false);
    }

    public void ConvertSettingPanel()
    {
        isSetting = !isSetting;
        settingPanel.SetActive(isSetting);
    }

    public void UpdateMonsterCount(string _monsterName)
    {
        // 여기서 몬스터 이름을 가져와서 카운트 업데이트
        string monsterName = _monsterName;
        int currentCount = achievement.GetCountForMonsterName(monsterName);
        achievement.UpdateCountForMonsterName(monsterName, currentCount + 1); // 몬스터 카운트 업데이트
    }

    public void UpdateUnitCount(string _unitName)
    {
        print("success");
        // 여기서 몬스터 이름을 가져와서 카운트 업데이트
        string unitName = _unitName;
        int currentCount = unitAchievement.GetCountForUnitName(unitName);
        print("Current : " + currentCount);
        unitAchievement.UpdateCountForUnitName(unitName, currentCount + 1);
    }

    public void OpenGameoverPanel()
    {
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
        gameOverPanel.SetActive(true);
        print("Gameover");
        achievement.SaveMonsterCountData();
        unitAchievement.SaveUnitCountData();
        
        


    }
}
