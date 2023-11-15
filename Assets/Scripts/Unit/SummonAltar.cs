using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SummonAltar : MonoBehaviour
{
    private Character player;
    public GameObject particleSystem;
    public GameObject spotLight;
    public GameObject summonFX;
    private bool isParticle = false;
    public float mana = 100f;
    private bool canSummon = false;
    public float detectionDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = CharacterManager.instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (SummonManager.instance.ReturnUnitCount() < 10)
        {
            //Can Summon
            if (distance <= detectionDistance)
            {
                if (canSummon)
                {
                    particleSystem.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        SummonManager.instance.selectAltar = gameObject;
                        SummonManager.instance.OpenSummonUI();
                        LightManager.instance.GlobalLight.SetActive(false);
                        
                        summonFX.SetActive(true);
                    }
                }
            }
            else
            {
                particleSystem.SetActive(false);
            }

            if (mana >= 100)
            {
                canSummon = true;
                spotLight.SetActive(true);
            }
            else
            {
                canSummon = false;
                spotLight.SetActive(false);
                mana += Time.deltaTime * 5;
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }


    }

    public void UseMana()
    {
        mana = 0f;
    }
}
