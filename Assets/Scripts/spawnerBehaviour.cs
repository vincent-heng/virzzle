using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class spawnerBehaviour : MonoBehaviour {
	public List<GameObject> spawns = new List<GameObject> ();
	public float[] coef = new float[3];
	public int nbItems;
	public float minX;
	public float minY;
	public float minZ;
	public float maxX;
	public float maxY;
	public float maxZ;


	// Use this for initialization
	void Start () {
		int n = Mathf.RoundToInt( Mathf.Sqrt ((nbItems + 1)/2 +1)) +1;
		float deltaX = (maxX - minX) / n;
		float deltaY = (maxY - minY) / n;
		float deltaZ = (maxZ - minZ) / 2;
		float radius = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ) /2;

		Random.seed = 13;
		for (int i = 0; i<nbItems; i++) {
			float v;
			float posX ;
			float posY ;
			float posZ ;
			bool isValid = false;
			Vector3 posVecteur;

			do{				
				v = Random.value * 100;
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
