﻿using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	float speed;
	Vector3 move;
	// Use this for initialization
	void Start () {
		speed = 1+ Random.Range(0,10)/10f;
		move = new Vector3(speed,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= move*Time.deltaTime; 
		if(transform.position.x<-13) transform.position +=new Vector3(36,0,0);
	}
}
