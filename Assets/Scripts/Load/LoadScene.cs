using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    private static LoadScene _instance;
    public static LoadScene instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<LoadScene>();
            }
            return _instance;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
