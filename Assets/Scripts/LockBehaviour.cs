using UnityEngine;
using System.Collections;

public class LockBehaviour : MonoBehaviour {

	// a modifier plus tard
	public bool open (GameObject key){
		return (key.CompareTag (this.tag));
	}
}
