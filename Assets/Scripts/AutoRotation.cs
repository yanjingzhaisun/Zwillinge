﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {

	public float rotationSpeed;


	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, rotationSpeed);
	}
}
