using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Animator animator;
    // Rigidbody
    private Rigidbody2D rigidbody2d;

    public Vector2 Move_Pos;

    public float moveSpeed;
    public AudioClip moveClip;
    public AudioClip hurtClip;
    private AudioSource audioPlayer;

    // Coroutine
    protected Coroutine MoveCoroutine;

    public Character Target_Player;

    public Character Target_Enemy;

    private float Move_Distance=0.15f;
    public bool IsMove = false;

    private bool IsMoveSoundPlaying = false;
    //
    public void Move(float range)
    {

        if (Vector2.Distance(rigidbody2d.position, Move_Pos) <= Move_Distance + range)
        {
            return;
        }

        Turn(Move_Pos);

        MoveCoroutine = StartCoroutine(move(range));
    }

    private IEnumerator move(float range)
    {
        while(Vector2.Distance(rigidbody2d.position, Move_Pos) > Move_Distance + range)
        {
            
                Vector2 dir = (Move_Pos - rigidbody2d.position).normalized;

                rigidbody2d.MovePosition(rigidbody2d.position + (dir * moveSpeed) * Time.fixedDeltaTime);
                yield return new WaitForEndOfFrame();

                //animation controll
                IsMove = true;
                animator.SetBool("Move", IsMove);

                if (gameObject.CompareTag("Player"))
                    //audio controll
                    if (!IsMoveSoundPlaying)
                    {
                        audioPlayer.clip = moveClip;
                        audioPlayer.Play();
                        IsMoveSoundPlaying = true;
                    }

        }
        IsMove = false;
        animator.SetBool("Move", IsMove);
        MoveCoroutine = null;
    }

    public void Turn(Vector2 pos)
    {

        float x = Mathf.Abs(transform.localScale.x);


        if (transform.position.x > pos.x)
            transform.localScale = new Vector3(x, x, x);
        if (transform.position.x < pos.x)
            transform.localScale = new Vector3(-x, x, x);
    }

    public void Hurt()
    {
        audioPlayer.PlayOneShot(hurtClip);
    }

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected virtual void DoUpdate() { }

    // Update is called once per frame
    void Update()
    {
        if(IsMoveSoundPlaying && !audioPlayer.isPlaying)
        {
            IsMoveSoundPlaying = false;
        }
        DoUpdate();
    }
}
