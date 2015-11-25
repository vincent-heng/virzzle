using UnityEngine;
using System.Collections;

public class LockBehaviour : MonoBehaviour {
	public GameObject key;
	public GameObject door;
	private bool isLocked = true;

	void OnCollisionEnter( Collision col)
	{
		if (col.gameObject.tag== "Player")
		{
			tryToOpen(GameObject.Find ("Player").GetComponent<playerBehaviour2>().handedObject);
		}
	}

	// a modifier plus tard
	public void tryToOpen (GameObject o){
		if (key.Equals (o)) {
			isLocked = false;
			GameObject.Find ("Player").GetComponent<playerBehaviour2>().handedObject = null;
            int lck;
            switch (gameObject.name)
            {
                case "SocleBleu":
                    lck = 0;
                    break;
                case "SocleVert":
                    lck = 2;
                    break;
                case "SocleJaune":
                    lck = 3;
                    break;
                case "SocleRouge":
                    lck = 1;
                    break;
                default:
                    lck = -1;
                    break;
            }
            GameObject.Find("GameManager").GetComponent<altarBehaviour>().lockHasUnlocked(lck);
			o.SetActive(false);
			//animation
		} else {

		}
	}

	public bool isLockUnlocked(){
		return !isLocked;
	}
}
