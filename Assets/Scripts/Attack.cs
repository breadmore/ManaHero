using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float Unit_Damage;
    // Start is called before the first frame update
    void Start()
    {
        Unit_Damage = GetComponentInParent<Character_Unit>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LivingEntity target = collision.GetComponent<LivingEntity>();

        
        if (collision.CompareTag("Monster"))
        {
            if (target != null)
            {
                target.OnDamage(Unit_Damage);
            }
        }

        
    }
}
