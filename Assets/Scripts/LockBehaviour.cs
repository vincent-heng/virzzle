using UnityEngine;
using System.Collections;

public class LockBehaviour : MonoBehaviour {
	public GameObject key;
	public GameObject door;

	void OnCollisionEnter( Collision col)
	{
		if (col.gameObject.tag== "Player")
		{
			tryToOpen(GameObject.Find ("Player").GetComponent<playerBehaviour>().handedObject);
		}
	}

	// a modifier plus tard
	public void tryToOpen (GameObject o){
		if (key.Equals (o)) {
			door.SetActive(false);
		} else {

		}
	}
}
