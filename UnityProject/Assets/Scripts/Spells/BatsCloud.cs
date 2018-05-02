using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    public class BatsCloud : MonoBehaviour
    {

        private void Start()
        {
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 15;
            // Destroy the bullet after 5 seconds
            Destroy(gameObject, 5.0f);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Health health = collider.gameObject.GetComponent<Health>();
                if (health)
                {
                    health.TakeDamage(50);
                }
            }
        }
    }
}