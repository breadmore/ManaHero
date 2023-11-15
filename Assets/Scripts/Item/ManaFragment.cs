using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFragment : MonoBehaviour, IDropItem
{
    public int manaMin = 10;
    public int manaMax = 20;
    public float manaAbsDistance = 2f;
    private float moveSpeed = 5f;
    private Vector3 directionToPlayer;
   public void Use()
    {
        int randMana = Random.Range(manaMin, manaMax);
        GameManager.instance.AddMana(randMana);

        Destroy(gameObject);
    }

    public void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Update()
    {
        if (Vector2.Distance(CharacterManager.instance.Player.transform.position, transform.position) <= manaAbsDistance)
        {
            Vector2 directionToPlayer = (CharacterManager.instance.Player.transform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
        }
    }
}
