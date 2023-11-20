using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{

    private Animator animator;
    private Renderer renderer;

    public float damage = 1f;
    public GameObject manaFragment;

    private float currentDamageTime =0f;
    private float damageCoolTime = 1.5f;

    private float avoidDistance = 5f;
    private Rigidbody2D rigidbody2D;
    private Character character;

    public bool isBoss = false;
    public float probability = 1f;


    private Vector2 initpos;
    private void Awake()
    {
        character = GetComponent<Character>();
        renderer = GetComponent<Renderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        initpos = transform.position; 
    }


    public override void OnDamage(float damage)
    {
        if (dead)
        {
            return;
        }
        //effect
        animator.SetTrigger("Hurt");
        character.Hurt();
        base.OnDamage(damage);
        animator.SetTrigger("Idle");
    }

    public override void Die()
    {
        base.Die();
        Vector2 pos = transform.position;
        GameObject dropItem = Instantiate(manaFragment,pos,transform.rotation);
        if(isBoss && Random.value < probability)
        {
            PromoteManager.instance.canPromote = true;
        }
        animator.SetTrigger("Die");
        gameObject.GetComponent<LivingEntity>().enabled = false;

        Invoke("DeactivateGameObject", 0.5f);
        string monsterName = RemoveCloneSuffix(gameObject.name);
        GameManager.instance.score++;
        GameManager.instance.UpdateMonsterCount(monsterName);
    }

    void DeactivateGameObject()
    {
        transform.position = initpos;
        dead = false;
        if (isBoss)
        {
            isBoss = false;
            InitHealth /= 2;
            damage = 1;
            transform.localScale = new Vector2(1f, 1f);
        }
        gameObject.SetActive(false);
    }

    private string RemoveCloneSuffix(string name)
    {
        const string cloneSuffix = "(Clone)";
        if (name.EndsWith(cloneSuffix))
        {
            return name.Substring(0, name.Length - cloneSuffix.Length);
        }
        return name;
    }


    public void SetBoss()
    {
        isBoss = true;
        InitHealth *= 2;
        damage = 2;
        transform.localScale = new Vector2(2f, 2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //AvoidOtherMonsters();
    }

    private void AvoidOtherMonsters()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidDistance, LayerMask.GetMask("Monster"));

        foreach (Collider2D collider in colliders)
        {
            print(collider.name);
            if (collider != GetComponent<Collider2D>())
            {
                Vector2 avoidDirection = (transform.position - collider.transform.position).normalized;
                Vector2 newPosition = (Vector2)transform.position + avoidDirection * avoidDistance * Time.deltaTime;
                rigidbody2D.MovePosition(newPosition);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LivingEntity player = collision.GetComponent<LivingEntity>();

        if (collision.CompareTag("Player") && player != null)
        {
            if (Time.time > currentDamageTime + damageCoolTime)
            {
                player.OnDamage(damage);
                currentDamageTime = Time.time;
            }
        }
    }
}
