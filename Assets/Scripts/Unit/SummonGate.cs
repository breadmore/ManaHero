using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonGate : MonoBehaviour
{
    public Character player;
    public GameObject particleSystem;
    public GameObject gateFX;
    private bool isParticle = false;
    public float detectionDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = CharacterManager.instance.Player;
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        //Can Summon
        if (distance <= detectionDistance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SummonManager.instance.audioSource.PlayOneShot(SummonManager.instance.SummonGateSound);
                Instantiate(gateFX, new Vector2(transform.position.x, transform.position.y+2f), Quaternion.identity);
                SummonManager.instance.SummonRandUnit(transform.position);
                Destroy(gameObject);
            }
        }

        if(SummonManager.instance.ReturnUnitCount() > 10)
        {
            Destroy(gameObject);
        }
    }
}
