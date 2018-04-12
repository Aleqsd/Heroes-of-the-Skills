using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.CompareTag("Enemy"))
        {
            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);
            }
        }
        Destroy(gameObject);
    }
}