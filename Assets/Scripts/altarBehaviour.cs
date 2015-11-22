using UnityEngine;
using System.Collections;

public class altarBehaviour : MonoBehaviour {
	public GameObject redLock;
	public GameObject blueLock;
	public GameObject yellowLock;
	public GameObject greenLock;

	// Update is called once per frame
	void Update () {
		if (redLock.GetComponent<LockBehaviour> ().isLockUnlocked ()
		    && blueLock.GetComponent<LockBehaviour> ().isLockUnlocked ()
		    && yellowLock.GetComponent<LockBehaviour> ().isLockUnlocked ()
		    && greenLock.GetComponent<LockBehaviour> ().isLockUnlocked ()) {
			print ("et c est la win");
		}
	}
}
