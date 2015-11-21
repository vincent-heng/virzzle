using UnityEngine;
using System.Collections;

public class doorBehavior : MonoBehaviour {
	private bool isLocked = true;

	public void unlockDoor(){	
		isLocked = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}
}
