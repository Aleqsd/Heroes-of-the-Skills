using UnityEngine;
using UnityEngine.Networking;

public class HealerController : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public GameObject specialBulletPrefab;
    public Transform bulletSpawn;
	public float specialFireRate = 10;
	private float nextFire;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		//Espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

		//C : Every 'specialFireRate' seconds (10 seconds by default)
		if (Input.GetKeyDown(KeyCode.C) && Time.time > nextFire)
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
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

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
			new Vector3(transform.position.x,0.15f,transform.position.z),
			bulletSpawn.rotation);

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(specialBullet);

		// Destroy the bullet after 2 seconds
		Destroy(specialBullet, 10.0f);
	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}