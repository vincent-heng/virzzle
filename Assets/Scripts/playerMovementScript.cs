﻿using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

    public float force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 orient = GameObject.Find("CenterEyeAnchor").transform.forward;
            GetComponent<Rigidbody>().AddForce(orient.normalized * force);

        }
	}

    void OnCollisionEnter( Collision col)
    {
        if(col.gameObject.tag== "world")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
