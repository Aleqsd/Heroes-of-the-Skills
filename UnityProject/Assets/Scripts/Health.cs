using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;
    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    private Animator anim;
    [HideInInspector] public bool dead;                // Has the bot been reduced beyond zero health yet?
    public AudioSource healthAudio;                   // The audio source to play.
    public AudioClip[] gettingHit;                      // Audio to play when the bot is getting hit.
    public AudioClip dying;                           // Audio to play when the bot is dying.
    public GameObject[] deathEffects;

    public float getHealth()                          // can get, not set
    {
        return currentHealth;
    }

    private void Awake()
    {

    }


    private void OnEnable()
    {
        dead = false;
    }

    private void Audio()
    {
        if (healthAudio)
        {
            if (!dead)
                healthAudio.clip = gettingHit[Random.Range(0,gettingHit.Length)];
            else
                healthAudio.clip = dying;

            if (!healthAudio.isPlaying)
            {
                healthAudio.Play();
            }
        }
    }



    void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void TakeDamage(int amount)
    {
        //if (!isServer)
        //    return;

        currentHealth -= amount;
        if (currentHealth <= 0 && !dead)
        {

            dead = true;
            if (destroyOnDeath)
            {
                Destroy(gameObject);
                if (deathEffects.Length > 0)
                    Destroy(Instantiate(deathEffects[Random.Range(0, deathEffects.Length)], new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0)), 3);
            }

            else
            {
                currentHealth = maxHealth;
            }
        }
        else
            anim.SetTrigger("isAttacked");

        Audio();
    }

    public void GetHealed(int amount)
    {
        //if (!isServer)
        //    return;
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

   
}