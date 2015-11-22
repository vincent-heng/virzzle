using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerBehaviour : MonoBehaviour {
	public float armLength = 20;
	public float dropRotationForce;
	public GameObject handedObject;
	public bool triggeredWithKeyboard;
	private GameObject centerEyeAnchor;
	private GameObject visor;
	private Vector3 lastVisorPosition;
	private float rightTrig = Input.GetAxisRaw("Right Trigger");
	private float leftTrig = Input.GetAxisRaw("Left Trigger");

	/// <summary>
	/// Gets the center eye anchor by memoization.
	/// </summary>
	/// <returns>The center eye anchor.</returns>
	private GameObject getCenterEyeAnchor() {
		if (centerEyeAnchor == null) {
			centerEyeAnchor = GameObject.Find ("CenterEyeAnchor");
		}
		return centerEyeAnchor;
	}

	/// <summary>
	/// Gets the visor by memoization.
	/// </summary>
	/// <returns>The visor.</returns>
	private GameObject getVisor() {
		if (visor == null) {
			visor = GameObject.Find ("Visor");
		}
		return visor;
	}
	
	/// <summary>
	/// Takes the item.
	/// </summary>
	/// <returns><c>true</c>, if item was taken, <c>false</c> otherwise.</returns>
	private bool takeItem() {
		if (handedObject != null) {
			return true; // Already handing an item
		}
		RaycastHit hit;
		Vector3 playerOrientation = getCenterEyeAnchor().transform.forward;
		Ray playerRay = new Ray(transform.position, playerOrientation);
		if (Physics.Raycast (playerRay, out hit, armLength)) {
			Collider col = hit.collider;
			if (col.tag == "key" || col.tag == "randomItem") {
				handedObject = col.gameObject;

				Transform parent = getVisor().transform;
				handedObject.transform.SetParent(parent, true);
				handedObject.GetComponent<Rigidbody>().isKinematic = true;
				float forwardSizeMod = col.transform.lossyScale.z * 13;
				float forwardDecalage = -7f + forwardSizeMod;
				float upSizeMod = col.transform.lossyScale.y * -11;
				float upDecalage = -1.5f + upSizeMod;
				handedObject.transform.localPosition = centerEyeAnchor.transform.forward*forwardDecalage + centerEyeAnchor.transform.up*upDecalage;

				return true; // Just took an item
			}
		}
		return false; // No item to take
	}

	/// <summary>
	/// Drops the item.
	/// </summary>
	/// <returns><c>true</c>, if item was droped, <c>false</c> otherwise.</returns>
	private bool dropItem() {
		if (handedObject == null) {
			return false;
		}
		handedObject.GetComponent<Rigidbody> ().isKinematic = false;
		Vector3 playerOrientation = getCenterEyeAnchor().transform.forward;
		handedObject.transform.parent = null;
		Rigidbody rgbd = handedObject.GetComponent<Rigidbody> ();
		Vector3 momentum = (visor.transform.position - lastVisorPosition)*130000f*Time.deltaTime;
		rgbd.AddForce (playerOrientation.normalized + momentum);
		rgbd.AddTorque ((Vector3.right + Vector3.forward).normalized * dropRotationForce);
		handedObject = null;
		return true;
	}

	/// <summary>
	/// Throws the item.
	/// </summary>
	/// <returns><c>true</c>, if item was thrown, <c>false</c> otherwise.</returns>
	private bool throwItem() {
		if (handedObject == null) {
			return false;
		}
		handedObject.GetComponent<Rigidbody> ().isKinematic = false;
		Vector3 playerOrientation = getCenterEyeAnchor().transform.forward;
		handedObject.transform.parent = null;
		Rigidbody rgbd = handedObject.GetComponent<Rigidbody> ();
		Vector3 momentum = (visor.transform.position - lastVisorPosition)*130000f*Time.deltaTime;
		rgbd.AddForce (playerOrientation.normalized * 70 + momentum);
		rgbd.AddTorque ((Vector3.right + Vector3.forward).normalized * dropRotationForce * 15);
		handedObject = null;
		return true;
	}
	
	void Update() {
		if (Input.GetKeyDown (KeyCode.Space) || rightTrig == 1 ) {
			if (!takeItem ()) {
				Debug.Log ("No item to catch...");
			} else {
				triggeredWithKeyboard = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.T) || (leftTrig == 1)) {
			if (!throwItem ()) {
				Debug.Log ("No item to throw...");
			} else {
				Debug.Log ("Pew !");
			}
		}

		if (Input.GetKeyUp (KeyCode.Space) || (rightTrig == 0 && handedObject != null && !triggeredWithKeyboard)) {
			if (dropItem()) {
				Debug.Log("Item dropped");
			} else {
				Debug.Log ("Nothing to drop...");
			}
		}
		lastVisorPosition = visor.transform.position;
	}
}
