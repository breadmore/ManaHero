using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightManager : MonoBehaviour
{
    private static LightManager _instance;
    public static LightManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LightManager>();
            }

            return _instance;
        }

    }

    public GameObject GlobalLight;
    private Light2D globalLightComponenet;

    public GameObject MonsterLight;
    // Start is called before the first frame update
    void Start()
    {
        globalLightComponenet = GlobalLight.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
