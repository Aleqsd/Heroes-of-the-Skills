using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		GameObject hit = collision.gameObject;
		Health health = hit.GetComponent<Health>();
		if (health != null)
		{
			health.TakeDamage(50);
		}

		Destroy (gameObject);
	}
}