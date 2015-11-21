using UnityEngine;
using System.Collections;

public class localMovement : MonoBehaviour {
	public int speed;
	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.Z))
		{
			rb.velocity =Vector3.forward;			
		}
		if (Input.GetKey(KeyCode.S))
		{
			rb.velocity = Vector3.back;			
		}
		if (Input.GetKey(KeyCode.Q))
		{
			rb.velocity = Vector3.left;			
		}
		if (Input.GetKey(KeyCode.D))
		{
			rb.velocity = Vector3.right;			
		}		
		if (Input.GetKey(KeyCode.R))
		{
			rb.velocity = Vector3.up;			
		}
		if (Input.GetKey(KeyCode.F))
		{
			rb.velocity = Vector3.down;			
		}
		if (Input.GetKey(KeyCode.W))
		{
			rb.velocity = Vector3.zero;			
		}
	
	}

}

