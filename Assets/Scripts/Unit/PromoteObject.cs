using UnityEngine;
using System.Collections;

public class PromoteObject : MonoBehaviour
{
    private Character player;
    public float detectionDistance = 3f;
    // Use this for initialization
    void Start()
    {
        player = CharacterManager.instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && PromoteManager.instance.canPromote)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if(distance <= detectionDistance)
            PromoteManager.instance.OpenPromoteUI();
        }

        if(PromoteManager.instance.canPromote && PromoteManager.instance.maxKnightUpgrade == PromoteManager.instance.knightUpgrade &&
            PromoteManager.instance.maxArcherUpgrade == PromoteManager.instance.archerUpgrade)
        {
            Destroy(gameObject);
        }
    }
}
