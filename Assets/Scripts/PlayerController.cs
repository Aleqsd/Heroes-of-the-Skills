using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public GameObject specialBulletPrefab;
    public Transform bulletSpawn;
	public float specialFireRate = 10;
	private float nextFire;
    [HideInInspector] public Health health;

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

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		//Espace
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdFire();
        }

		//C : Every 'specialFireRate' seconds (10 seconds by default)
		if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextFire)
		{
			nextFire = Time.time + specialFireRate;
			CmdSpecialFire();
			Debug.Log("Firing once every 10s");
		}
    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 60;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

	[Command]
	void CmdSpecialFire()
	{
		// Create the Bullet from the Bullet Prefab
		var specialBullet = (GameObject)Instantiate(
			specialBulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

        // Add velocity to the bullet
        specialBullet.GetComponent<Rigidbody>().velocity = specialBullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(specialBullet);

		// Destroy the bullet after 2 seconds
		Destroy(specialBullet, 2.0f);
	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        health = GetComponent<Health>();
        gameObject.AddComponent<AudioListener>();
    }
}