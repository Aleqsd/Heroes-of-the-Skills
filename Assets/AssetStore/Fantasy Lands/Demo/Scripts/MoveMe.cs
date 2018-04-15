using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {
    public float speed = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
                transform.Translate(Vector3.forward * Time.deltaTime*speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed );
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-Vector3.left * Time.deltaTime * speed);
        }





    }
}
