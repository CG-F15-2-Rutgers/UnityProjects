﻿using UnityEngine;
using System.Collections;

public class RotateOnSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,90, 0) * Time.deltaTime);
	}
}