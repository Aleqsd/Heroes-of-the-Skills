using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseRotate : MonoBehaviour {
    public Transform target;
    public float RotationSpeed = 1f;
    private bool mLeft = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target);

        if (Input.GetAxis("Mouse X") > 0)
            mLeft = true;
        if (Input.GetAxis("Mouse X") < 0)
            mLeft = false;


        if (mLeft==true)   
            transform.Translate(Vector3.left *RotationSpeed* Time.deltaTime);
        else
            transform.Translate(Vector3.right * RotationSpeed * Time.deltaTime);
    }
}
