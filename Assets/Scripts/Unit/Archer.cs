using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Transform shootPos;
    public Arrow arrowPrefab;
    public Vector2 enemyPos;
    
    // Start is called before the first frame update
    public void Shoot()
    {
        Character_Unit archer = transform.GetComponent<Character_Unit>();

        if(archer.Target_Enemy == null)
        {
            return;
        }
            enemyPos = archer.Target_Enemy.transform.position;
            Arrow arrow;
            arrow = arrowPrefab;
            arrow.enemyPos = enemyPos;
            arrow.damage = archer.damage;
            Instantiate(arrow, shootPos);
        
        //Instantiate(arrow, shootPos.position, Quaternion.identity);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
