
using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float InitHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action onDeath;
    

    protected virtual void OnEnable()
    {
        dead = false;
        health = InitHealth;
        print("hello");
    }


    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            // 이미 사망한 경우 체력을 회복할 수 없음
            return;
        }

        // 체력 추가
        health += newHealth;
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }


    public virtual void Die()
    {
        if(onDeath != null)
        {
            onDeath();
        }

        dead = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
