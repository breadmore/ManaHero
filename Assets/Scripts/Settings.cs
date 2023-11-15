using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public AudioSource soundPlayPoint;
    //save
    public Slider soundSlider;
    //save
    public GameObject checkImage;

    public bool isBgmCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        LoadSetting();
        InitSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBGM()
    {
        isBgmCheck = !isBgmCheck;
        checkImage.SetActive(isBgmCheck);
    }

    private void InitSetting()
    {
        soundPlayPoint.mute = !isBgmCheck;
        soundPlayPoint.volume = soundSlider.value;
    }

    public void SaveSetting()
    {
        PlayerPrefs.SetFloat("SOUND", soundSlider.value);
        PlayerPrefs.SetInt("BGM", isBgmCheck ? 1: 0);
        PlayerPrefs.Save();

        InitSetting();
    }


    public void LoadSetting()
    {
        int bgm = PlayerPrefs.GetInt("BGM");
        soundSlider.value = PlayerPrefs.GetFloat("SOUND");
        isBgmCheck = (bgm == 1);
        checkImage.SetActive(isBgmCheck);
    }
    public void LoadMain()
    {
        LoadScene.instance.LoadMain();
    }

}
