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


    float timer = 0;

    soundManager soundMng;

    void Start()
    {
        soundMng = GameObject.Find("GameManager").GetComponent<soundManager>();
    }

    void Update()
    {
        /*Debug.Log("FLOOD must delete theis function. exists juste for test;");
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
        }*/

        timer += Time.deltaTime;
        if (timer > 10)
            lockHasUnlocked((int)lockColor.blue);
        if (timer > 30)
            lockHasUnlocked((int)lockColor.green);
        if (timer > 60)
            lockHasUnlocked((int)lockColor.yellow);
        if (timer > 90)
            lockHasUnlocked((int)lockColor.red);
    }

	int unlockedCounter = 0;
	private int[] unlockedOrder = new int[4]{-1,-1,-1,-1};




	public void lockHasUnlocked(int lck){
        Debug.Log((int)lck);
		unlockedOrder [unlockedCounter] = (int)lck;
        soundMng.handleCrystalUnlock(unlockedOrder);
		unlockedCounter++;

        if (allUnlocked())
        {
            //fin du game;
            GameObject.Find("Player").GetComponent<playerMovementScript>().enabled = false;
            GameObject.Find("Player").GetComponent<playerBehaviour2>().enabled = false;
            Debug.Log("thanks for playing");
        }
	}

	public bool allUnlocked(){
		return unlockedCounter == 4;
	}
}
