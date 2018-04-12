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
    

    Animator animator;
    [HideInInspector] public bool dead;                // Has the bot been reduced beyond zero health yet?
    public AudioSource healthAudio;                   // The audio source to play.
    public AudioClip gettingHit;                      // Audio to play when the bot is getting hit.
    public AudioClip dying;                           // Audio to play when the bot is dying.
    public GameObject[] bloodPrefabs;                 // Blood to spill when getting hit
    public GameObject[] bloodEffects;

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
                healthAudio.clip = gettingHit;
            else
                healthAudio.clip = dying;

            if (!healthAudio.isPlaying)
            {
                healthAudio.Play();
            }
        }
    }

    private void SpillBlood()
    {
        if (bloodEffects.Length > 0 && bloodEffects.Length > 0)
        {
            Vector3 randomPositionAround = transform.position;
            randomPositionAround.x += Random.Range(-5, 5);
            randomPositionAround.z += Random.Range(-5, 5);
            Destroy(Instantiate(bloodPrefabs[Random.Range(0, bloodPrefabs.Length)], randomPositionAround, new Quaternion(0, 0, 0, 0)), 5);
            Destroy(Instantiate(bloodEffects[Random.Range(0, bloodEffects.Length)], new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0)), 3);
            //Debug.Log("Spill");
        }
        // TODO : spill in the opposite position to the impact
    }

    void Start()
    {
        animator = GetComponent<Animator>();

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
            }

            else
            {
                currentHealth = maxHealth;


            }
        }

        SpillBlood();
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