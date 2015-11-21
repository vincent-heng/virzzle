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
		Random.seed = 13;
		for (int i = 0; i<nbItems; i++) {
			float v = Random.value * 100;
			if (v<=coef[0]){
				GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(spawns[0]);
				print (v);
				o.transform.position = new Vector3(0,0,0);			
			}else if (v<=coef[0]+coef[1]){
				GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(spawns[1]);
				print (v);
				o.transform.position = new Vector3(1,0,0);
			}else{
				GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(spawns[2]);
				print (v);		
				o.transform.position = new Vector3(2,0,0);
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
