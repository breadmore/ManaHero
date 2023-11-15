using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Unit : Character
{
    public DeployPosition DeployPosition;
    public CircleCollider2D AttackRange;
    private bool CantAction = false;

    public float InRangeDistance = 6.5f;
    public int damage;
    public float attackSpeed = 3f;
    public float attackRange;
    private bool IsTargeting = false;
    public float followAngle;
    public float followDistance;


    private bool isDead = false;
    private float currentAttackTime = 0f;


    private float currentTargetCoolTime = 0f;
    private const float targetCoolTime = .5f;

    public UnitType unitType;
    public UnitBase unitBase;

    private AudioSource audioSource;
    public AudioClip attackClip;

    //
    public void Hired()
    {
        gameObject.tag = "Unit";
    }

    protected override void Init()
    {
        base.Init();
        audioSource = GetComponent<AudioSource>();

        AttackRange.radius = attackRange; 
        Target_Player = CharacterManager.instance.Player;
        Target_Enemy = null;
    }

    //
    private void FollowPlayer()
    {
        Vector2 pos = transform.position;
            pos = Target_Player.transform.position;
            Move_Pos = pos;

            Move(followDistance);
    }

    private void OutRange()
    {
        Vector2 playerPos = CharacterManager.instance.Player.transform.position;
        if (Vector2.Distance(playerPos, transform.position)>=InRangeDistance) 
        {

                TargetOut();
                currentTargetCoolTime = Time.time;


        }
            
    }

    private void FollowMonster()
    {
        if (Target_Enemy == null) return;
        Vector2 pos = transform.position;
        pos = Target_Enemy.transform.position;
        Move_Pos = pos;

        Move(attackRange);
    }
    public void Attack()
    {
        if (Target_Enemy == null)
        {
            //IsMove = true;
            //animator.SetBool("Move", IsMove);
            return;
        }

        //target dead
        //if()
        
        float distance = Vector2.Distance(Target_Enemy.transform.position, transform.position);
        if (distance <= attackRange + 0.15f && CantAction ==false)
        {
            if (currentAttackTime + attackSpeed <= Time.time)
            {
                currentAttackTime = Time.time;
                CantAction = true;
                animator.SetTrigger("Attack");
                
            }
        }
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(attackClip);
    }
    public void EndAttack()
    {
        CantAction = false;
        animator.SetTrigger("Idle");
    }

    //
    private void Targeting(GameObject monster)
    {
        Character target_monster = monster.GetComponent<Character>();
        if (target_monster != null)
        {
            IsTargeting = true;
            Target_Enemy = target_monster;
        }
    }

    private void TargetOut()
    {
        Target_Enemy = null;
        IsTargeting = false;
    }

    private void MonsterDeadCheck()
    {
        try
        {
            LivingEntity target_living = Target_Enemy.GetComponent<LivingEntity>();

        }
        catch
        {
            TargetOut();
        }
    }
    //
    private void UnitAction()
    {
        if (CantAction) return;

        //IsMove = true;
        //animator.SetBool("Move", IsMove);

        Vector2 pos = transform.position;
        if(IsTargeting == true )
        {
            MonsterDeadCheck();
            FollowMonster();
            Attack();
        }
        else
        {

            CantAction = false;
            FollowPlayer();
        }

        OutRange();
    }

    private void UnitAction_V2()
    {
        if (CantAction) return;

        //IsMove = true;
        //animator.SetBool("Move", IsMove);
        //transform.position = DeployPosition.transform.position;
        Move_Pos = DeployPosition.transform.position;

        if (IsTargeting == true)
        {
            MonsterDeadCheck();
            Attack();
        }
        if (!CantAction)
        {
            Move(0);
        }
        OutRange();
    }

    protected override void DoUpdate()
    {
        if (CantAction)
        {
            IsMove = false;
        }

        if (GameManager.instance.dead)
        {
            if (!isDead)
            {
                animator.SetTrigger("Die");
                isDead = true;
            }
        }
        else
        {
            UnitAction_V2();
        }
        //UnitAction();
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //
        if (collision.CompareTag("Monster"))
        {
            if(currentTargetCoolTime+targetCoolTime < Time.time)
            {
                Targeting(collision.gameObject);
            }

        }
    }

    public void Charge()
    {
        StartCoroutine(DashToMonster());
    }

    private IEnumerator DashToMonster()
    {
        float dashSpeed = 5f;
        Vector3 directionToMonster = (Target_Enemy.transform.position - transform.position).normalized;
        Vector3 dashTargetPosition = transform.position + directionToMonster * 2.0f;

        float distanceToTarget = Vector3.Distance(transform.position, dashTargetPosition);
        float dashDuration = distanceToTarget / dashSpeed;

        float elapsedTime = 0.0f;
        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            transform.position = Vector3.Lerp(transform.position, dashTargetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = dashTargetPosition;
    }

}
