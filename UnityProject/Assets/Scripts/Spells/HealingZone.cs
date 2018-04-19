using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour {

    private void Start()
    {
        //transform.position += new Vector3(10,0,10);
        Destroy(gameObject, 6);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<Health>().GetHealed(1);
    }
}
