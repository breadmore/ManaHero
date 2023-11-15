using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerHealth : LivingEntity
{
    private static PlayerHealth _instance;
    public static PlayerHealth instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerHealth>();
            return _instance;
        }
    }
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    public CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin m_noise;
    public AudioClip deathClip;
    public AudioClip hitClip;
    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;

    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return InitHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();

    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        ClampHealth();

    }

    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
            playerAnimator.SetTrigger("Hurt");
            Shake();
            base.OnDamage(damage);
            ClampHealth();
            playerAnimator.SetTrigger("Idle");
            
        }

    }
    public void AddHealth()
    {
        if (InitHealth < maxTotalHealth)
        {
            InitHealth += 1;
            health = InitHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    public override void Die()
    {
        base.Die();

        playerAudioPlayer.PlayOneShot(deathClip);

        playerAnimator.SetTrigger("Die");

        Invoke("GameOver", 1f);

    }

    private void GameOver()
    {
        GameManager.instance.dead = true;
        GameManager.instance.OpenGameoverPanel();
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, InitHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    public void Shake()
    {
        m_noise.m_AmplitudeGain = 1f;
        Invoke(nameof(StopShaking), 0.5f);
    }

    private void StopShaking()
    {
        m_noise.m_AmplitudeGain = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_noise = cinemachine.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dead)
        {
            IDropItem item = collision.GetComponent<IDropItem>();
            {
                if(item != null)
                {
                    item.Use();
                }
            }
        }
    }
}
