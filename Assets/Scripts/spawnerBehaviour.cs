using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

public class spawnerBehaviour : MonoBehaviour {
	public List<GameObject> spawns = new List<GameObject> ();
	public float[] coef = new float[3];
	public int nbItems;




	// Use this for initialization
	void Start () {
		Vector3 centre = GetComponent<BoxCollider>().center;
		Vector3 coords = GetComponent<BoxCollider>().size;
		
		float minX = centre.x - coords.x / 2;
		float minY = centre.y - coords.y / 2;
		float minZ = centre.z - coords.z / 2;
		float maxX = centre.x + coords.x / 2;
		float maxY = centre.y + coords.y / 2;
		float maxZ = centre.z + coords.z / 2;

		int n = Mathf.RoundToInt( Mathf.Sqrt ((nbItems + 1)/2 +1)) +1;
		float deltaX = (maxX - minX) / n;
		float deltaY = (maxY - minY) / n;
		float deltaZ = (maxZ - minZ) / 2;
		float radius = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ) /2;

		UnityEngine.Random.seed = (int) DateTime.Now.Ticks & 0x0000FFFF;

		for (int i = 0; i<nbItems; i++) {
			float v;
			float posX ;
			float posY ;
			float posZ ;
			bool isValid = false;
			Vector3 posVecteur;

			do{				
				v = UnityEngine.Random.value * 100;
				int c = Mathf.RoundToInt(v) % (2*n*n) +1;
				posX = minX + deltaX/2 + ((c-1)%n) * deltaX;
				posY = minY + deltaY/2 + (c/n) * deltaY;
				posZ = minZ + deltaZ/2 + (c%2) * deltaZ;
				posVecteur = new Vector3(posX, posY, posZ);
				isValid = Physics.CheckSphere(posVecteur, radius)
					&& (posX>minX) && (posY>minX) && (posZ>minZ)
					&& (posX<maxX) && (posY < maxY) && ( posZ<maxZ);
				print (c);
			}while (!isValid);
			
			print ("good");

			if (v<=coef[0]){
				GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(spawns[0]);
				o.transform.position = posVecteur;			
			}else if (v<=coef[0]+coef[1]){
				GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(spawns[1]);
				o.transform.position = posVecteur;
			}else{
				GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(spawns[2]);
				o.transform.position = posVecteur;
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
