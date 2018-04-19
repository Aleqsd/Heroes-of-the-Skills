using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject[] spells;
    public float[] spellCooldowns;
    protected float[] nextSpell;
    public Transform bulletSpawn;
    public Animator anim;
    [HideInInspector] public Health health;

    protected void Start()
    {
        anim = GetComponent<Animator>();

        nextSpell = new float[spellCooldowns.Length];

        PlayerPrefs.SetInt("form", 0); // Start with light form
    }

    void Update()
    {
        

        if (!isLocalPlayer)
        {
            this.transform.GetChild(0).GetComponent<Camera>().enabled = false;
            return;
        }

        this.transform.GetChild(0).GetComponent<Camera>().enabled = true;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            
            anim.SetFloat("Input X", Input.GetAxis("Vertical"));
            anim.SetFloat("Input Z", Input.GetAxis("Horizontal"));
            anim.SetBool("Moving", true);
        }
        else
            anim.SetBool("Moving", false);
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        SpellInput();

    }

    protected virtual void SpellInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && spells[0] != null && Time.time > nextSpell[0])
        {
            nextSpell[0] = Time.time + spellCooldowns[0];
            CmdSpell(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && spells[1] != null && Time.time > nextSpell[1])
        {
            nextSpell[1] = Time.time + spellCooldowns[1];
            CmdSpell(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && spells[2] != null && Time.time > nextSpell[2])
        {
            nextSpell[2] = Time.time + spellCooldowns[2];
            CmdSpell(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && spells[3] != null && Time.time > nextSpell[3])
        {
            nextSpell[3] = Time.time + spellCooldowns[3];
            CmdSpell(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && spells[4] != null && Time.time > nextSpell[4])
        {
            nextSpell[4] = Time.time + spellCooldowns[4];
            CmdSpell(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && spells[5] != null && Time.time > nextSpell[5])
        {
            nextSpell[5] = Time.time + spellCooldowns[5];
            CmdSpell(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && spells[6] != null && Time.time > nextSpell[6])
        {
            nextSpell[6] = Time.time + spellCooldowns[6];
            CmdSpell(6);
        }
    }


    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    protected void CmdSpell(int spell)
    {
        

        GetComponent<NetworkAnimator>().SetTrigger("Attack1Trigger");
        GetComponent<NetworkAnimator>().animator.ResetTrigger("Attack1Trigger");

        GameObject spellInstance = (GameObject)Instantiate(
            spells[spell],
            bulletSpawn.position,
            bulletSpawn.rotation);
        

        // Spawn the spellInstance on the Clients
        NetworkServer.Spawn(spellInstance);

        
    }


    public override void OnStartLocalPlayer()
    {
        //GetComponent<MeshRenderer>().material.color = Color.blue;
        health = GetComponent<Health>();
        gameObject.AddComponent<AudioListener>();
    }
}