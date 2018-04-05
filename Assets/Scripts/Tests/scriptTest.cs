using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        if (Physics.SphereCast(origin, 1, transform.forward, out hit,10, LayerMask.GetMask("Eatable")))
        {
            Debug.Log("Hit : " + hit);
        }
        else
        {
            //Debug.Log("NOT HIT");
        }
    }
}
