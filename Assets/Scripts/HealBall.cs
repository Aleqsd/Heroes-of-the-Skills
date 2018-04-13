using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBall : MonoBehaviour {

    private void Start()
    {

        // Destroy the bullet after 5 seconds
        Destroy(gameObject, 5.0f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        { 
            Health health = collider.gameObject.GetComponent<Health>();
            if (health)
            {
                health.GetHealed(10);
            }
        }
        Destroy(gameObject);
    }
}
