using UnityEngine;
using System.Collections;

public class buttonControl_script : MonoBehaviour 
{

	Animator anim;

	void Awake ()
	{
		anim = GetComponentInChildren<Animator>();
	}

	public void CrippledWalk ()
	{
		anim.SetBool("crippled", !(anim.GetBool("crippled")));
		anim.SetBool("isIdle", false);
	}

	public void Idle ()
	{
		anim.SetBool("isIdle", true);
		anim.SetBool("isRun", false);
		anim.SetBool("crippled", false);
		anim.SetBool("dancing", false);
	}

	public void Run ()
	{
		anim.SetBool("isRun",!(anim.GetBool("isRun")));
		anim.SetBool("isIdle", false);

	}

	public void Dance()
	{
		anim.SetBool ("dancing", !(anim.GetBool("dancing")));
	}
}
