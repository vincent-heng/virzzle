using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerBehaviour : MonoBehaviour {
	public float armLength = 20;
	public float dropRotationForce;
	public GameObject handedObject;
	public bool triggeredWithKeyboard;
	private GameObject centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
	private GameObject visor = GameObject.Find("Visor");

	/**
	 * Returns true if catching an item
	 */
	private bool takeItem() {
		if (handedObject != null) {
			return true; // Already handing an item
		}
		RaycastHit hit;
		Vector3 playerOrientation = centerEyeAnchor.transform.forward;
		Ray playerRay = new Ray(transform.position, playerOrientation);
		if (Physics.Raycast (playerRay, out hit, armLength)) {
			Collider col = hit.collider;
			if (col.tag == "key") {
				handedObject = col.gameObject;

				Transform parent = visor.transform;
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

	private bool dropItem() {
		if (handedObject == null) {
			return false;
		}
		handedObject.GetComponent<Rigidbody> ().isKinematic = false;
		Vector3 playerOrientation = centerEyeAnchor.transform.forward;
		handedObject.transform.parent = null;
		Rigidbody rgbd = handedObject.GetComponent<Rigidbody> ();
		rgbd.AddForce (playerOrientation.normalized);
		rgbd.AddTorque ((Vector3.right + Vector3.forward).normalized * dropRotationForce);
		handedObject = null;
		return true;
	}

	void Update() {
        float rightTrig = Input.GetAxisRaw("Right Trigger");

		if (Input.GetKeyDown (KeyCode.Space) || rightTrig == 1 ) {
			if (!takeItem ()) {
				Debug.Log ("No item to catch...");
			} else {
				triggeredWithKeyboard = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space) || (rightTrig == 0 && handedObject != null && !triggeredWithKeyboard)) {
			if (dropItem()) {
				Debug.Log("Item dropped");
			} else {
				Debug.Log ("Nothing to drop...");
			}
		}
	}
}
