﻿using UnityEngine;
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

	int unlockedCounter = 0;
	private int[] unlockedOrder = new int[4]{-1,-1,-1,-1};

	public void lockHasUnlocked(lockColor lck){
		unlockedOrder [(int)lck] = unlockedCounter;
		unlockedCounter++;
	}

	public bool allUnlocked(){
		return unlockedCounter == 4;
	}



	// Update is called once per frame
	void Update () {

	}
}
