using UnityEngine;
using System.Collections;

public enum lockColor
{
	blue,
	red,
	green,
	yellow
}

public class altarBehaviour : MonoBehaviour {

	public AudioSource source;

    soundManager soundMng;

    void Start()
    {
        soundMng = GameObject.Find("GameManager").GetComponent<soundManager>();
    }

    void Update()
    {
        Debug.Log("FLOOD must delete theis function. exists juste for test;");
        if (Input.GetKeyDown(KeyCode.P))
        {
            lockHasUnlocked(lockColor.blue);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            lockHasUnlocked(lockColor.green);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            lockHasUnlocked(lockColor.red);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            lockHasUnlocked(lockColor.yellow);
        }
    }

	int unlockedCounter = 0;
	private int[] unlockedOrder = new int[4]{-1,-1,-1,-1};

	public void lockHasUnlocked(lockColor lck){
        Debug.Log((int)lck);
		unlockedOrder [unlockedCounter] = (int)lck;
        soundMng.handleCrystalUnlock(unlockedOrder);
		unlockedCounter++;
	}

	public bool allUnlocked(){
		return unlockedCounter == 4;
	}
}
