using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerBehaviour : MonoBehaviour {
	
	public float force;
	private List<GameObject> inventory = new List<GameObject>();

	void OnTriggerEnter( Collider col)
	{		
		if (col.gameObject.tag == "key") {
			inventory.Add(col.gameObject);
			col.gameObject.SetActive(false);
		}
	}
}
