using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour 
{

	public float jumpSpeed = 600.0f;
	public bool grounded = false;
	public bool doubleJump = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private Animator anim;
	public Rigidbody rb;
	public float vSpeed;

	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
		anim.SetBool("isIdle", true);
	}
	void Start ()
	{
		
	}
	void FixedUpdate () 
	{
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		vSpeed = rb.velocity.y;
        anim.SetFloat ("vSpeed", vSpeed);
	}
	void Update () 
	{
		if (Input.GetKeyDown("space") && anim.GetBool("isIdle"))
		{
			Jump();
		}
	}

	public void Jump ()
	{
		if (grounded && rb.velocity.y == 0)
		{
			anim.SetTrigger("isJump");
            rb.AddForce(0,jumpSpeed,0, ForceMode.Impulse);
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
	}

}
