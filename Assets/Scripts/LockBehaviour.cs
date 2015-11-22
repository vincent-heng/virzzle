using UnityEngine;
using System.Collections;

public class LockBehaviour : MonoBehaviour {
	public GameObject key;
	public GameObject door;
	private bool isLocked = true;

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
			isLocked = false;
			GameObject.Find ("Player").GetComponent<playerBehaviour>().handedObject = null;
			o.SetActive(false);
			//animation
		} else {

		}
	}

	public bool isLockUnlocked(){
		return !isLocked;
	}
}
