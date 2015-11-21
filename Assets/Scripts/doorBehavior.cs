using UnityEngine;
using System.Collections;

public class doorBehavior : MonoBehaviour {
	private bool isLocked = true;

	public void unlockDoor(){
		isLocked = false;
	}

	public void openDoor(){
		if (!isLocked)
			GetComponent<MeshRenderer> ().enabled = false;
	}

	public void closeDoor(){
		if (!isLocked)			
			GetComponent<MeshRenderer> ().enabled = true;
	}


}
