using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerBehaviour : MonoBehaviour {
	public float armLength = 20;
	public float dropRotationForce;
	public GameObject handedObject;
	public bool triggeredWithKeyboard;
    private bool canGrab = true;
    private GameObject centerEyeAnchor;
	private GameObject visor;
	private Vector3 lastVisorPosition;

    //soundManager
    soundManager soundMng;

	private GameObject getCenterEyeAnchor() {
		if (centerEyeAnchor == null) {
			centerEyeAnchor = GameObject.Find ("CenterEyeAnchor");
		}
		return centerEyeAnchor;
	}

    void Start()
    {
        soundMng = GameObject.Find("GameManager").GetComponent<soundManager>();
    }


	private GameObject getVisor() {
		if (visor == null) {
			visor = GameObject.Find ("Visor");
		}
		return visor;
	}
	

	private bool takeItem() {
            if (handedObject != null)
            {
                return true; // Already handing an item
            }
            RaycastHit hit;
            Vector3 playerOrientation = getCenterEyeAnchor().transform.forward;
            Ray playerRay = new Ray(transform.position, playerOrientation);
            if (Physics.Raycast(playerRay, out hit, armLength))
            {
                Collider col = hit.collider;
                if (col.tag == "key" || col.tag == "randomItem")
                {
                    handedObject = col.gameObject;

                    Transform parent = getVisor().transform;
                    handedObject.transform.SetParent(parent, true);
                    
                    float forwardSizeMod = col.transform.lossyScale.z * 13;
                    float forwardDecalage = -7f + forwardSizeMod;
                    float upSizeMod = col.transform.lossyScale.y * -11;
                    float upDecalage = -1.5f + upSizeMod;
                    handedObject.transform.localPosition = centerEyeAnchor.transform.forward * forwardDecalage + centerEyeAnchor.transform.up * upDecalage;

                    //handedObject.GetComponent<Rigidbody>().isKinematic = true;
                    soundMng.Play(soundManager.soundTypes.grabObject);
                    canGrab = false;
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
		Vector3 playerOrientation = getCenterEyeAnchor().transform.forward;
		handedObject.transform.parent = null;
		Rigidbody rgbd = handedObject.GetComponent<Rigidbody> ();
		Vector3 momentum = (visor.transform.position - lastVisorPosition)*130000f*Time.deltaTime;
		rgbd.AddForce (playerOrientation.normalized + momentum);
		rgbd.AddTorque ((Vector3.right + Vector3.forward).normalized * dropRotationForce);
		handedObject = null;
        soundMng.Play(soundManager.soundTypes.dropObject);
        return true;
	}


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
        soundMng.Play(soundManager.soundTypes.pushObject);
        return true;
	}
	
	void Update() {

        float rightTrig = Input.GetAxisRaw("Right Trigger");
        float leftTrig = Input.GetAxisRaw("Left Trigger");

        if (!canGrab)
        {
            if ( rightTrig == 0)
            {
                canGrab = true;
                dropItem();
            }else if (leftTrig == 1)
            {
                throwItem();
            }
        }
        else
        {
            if (rightTrig == 1)
            {
                takeItem();
            }

        }
        lastVisorPosition = getVisor().transform.position;
        /*
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!takeItem ()) {
				Debug.Log ("No item to catch...");
			} else {
				triggeredWithKeyboard = true;
			}
		}
        if (rightTrig == 1)
        {
            if (!takeItem())
            {
                Debug.Log("No item to catch...");
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
                triggeredWithKeyboard = false;
			} else {
				Debug.Log ("Nothing to drop...");
			}
		}*/
    }
}
