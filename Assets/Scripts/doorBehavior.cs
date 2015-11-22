using UnityEngine;
using System.Collections;

public class doorBehavior : MonoBehaviour {
	private bool isLocked = true;

	/*
	public void unlockDoor(){	
		isLocked = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}*/

	/*
	public bool isDoorUnLocked(){
		return !isLocked;
	}*/

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			open ();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			close ();
		}
	}

	private void open(){
		GetComponent<Renderer> ().enabled = false;

	}

		private void close(){
		GetComponent<Renderer> ().enabled = true;
	}
}
