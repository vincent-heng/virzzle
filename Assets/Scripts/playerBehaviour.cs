﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerBehaviour : MonoBehaviour {
	public float armLength = 20;
	public float dropRotationForce;
	public GameObject handedObject;

	/**
	 * Returns true if catching an item
	 */
	private bool takeItem() {
		if (handedObject != null) {
			return true; // Already handing an item
		}
		RaycastHit hit;
		Vector3 playerOrientation = GameObject.Find("CenterEyeAnchor").transform.forward;
		Ray playerRay = new Ray(transform.position, playerOrientation);
		if (Physics.Raycast (playerRay, out hit, armLength)) {
			Collider col = hit.collider;
			if (col.tag == "key") {
				handedObject = col.gameObject;

				Transform parent = GameObject.Find("Visor").transform;
				handedObject.transform.SetParent(parent, true);
				handedObject.GetComponent<Rigidbody>().isKinematic = true;
				float forwardDecalage = -7f;
				float upDecalage = -1.5f;
				handedObject.transform.localPosition = GameObject.Find("CenterEyeAnchor").transform.forward*forwardDecalage + GameObject.Find("CenterEyeAnchor").transform.up*upDecalage;

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
		Vector3 playerOrientation = GameObject.Find("CenterEyeAnchor").transform.forward;
		handedObject.transform.parent = null;
		Rigidbody rgbd = handedObject.GetComponent<Rigidbody> ();
		rgbd.AddForce (playerOrientation.normalized);
		rgbd.AddTorque ((Vector3.right + Vector3.forward).normalized * dropRotationForce);
		handedObject = null;
		return true;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!takeItem ()) {
				Debug.Log ("No item to catch...");
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (dropItem()) {
				Debug.Log("Item dropped");
			} else {
				Debug.Log ("Nothing to drop...");
			}
		}
	}

	void OnCollisionEnter( Collision other)
	{
		if(other.collider.tag == "door")
		{
			other.collider.GetComponent<doorBehavior>().openDoor();
		}     
	}

	
	void OnCollisionExit(Collision other)
	{
		if (other.collider.tag == "door")
		{
			other.collider.GetComponent<doorBehavior>().closeDoor();

		}
	}
}
