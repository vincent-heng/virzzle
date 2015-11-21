﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerBehaviour : MonoBehaviour {
	public float armLength = 20;
	public GameObject handedObject = new GameObject();


	private bool takeItem() {
		RaycastHit hit;
		Vector3 playerOrientation = GameObject.Find("CenterEyeAnchor").transform.forward;
		Ray playerRay = new Ray(transform.position, playerOrientation);
		if (Physics.Raycast (playerRay, out hit, armLength)) {
			Collider col = hit.collider;
			if (col.tag == "key") {
				handedObject = col.gameObject;
				Transform parent = GameObject.Find("Visor").transform;
				handedObject.transform.SetParent(parent, false);
				handedObject.transform.localPosition = new Vector3(0,0,0);
				//handedObject.SetActive(false);
				return true;
			}
		}
		return false;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (takeItem ()) {
				Debug.Log("Item catched !");
			} else {
				Debug.Log ("No item to catch...");
			}
		}
	}
}
