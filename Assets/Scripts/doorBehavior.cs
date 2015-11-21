using UnityEngine;
using System.Collections;

public class doorBehavior : MonoBehaviour {
	private bool isLocked = true;
	private bool isClosed = true;

	void Update (){
		this.gameObject.SetActive(isClosed);
	}

	public void unlockDoor(){
		isLocked = false;
	}

	public void openDoor(){
		if (!isLocked)
			isClosed = false;
	}

	public void closeDoor(){
		if (!isLocked)
			isClosed = true;
	}


}
