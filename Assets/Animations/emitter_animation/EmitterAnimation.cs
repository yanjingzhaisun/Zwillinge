using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterAnimation : MonoBehaviour {

	[SerializeField] GameObject br1;
	[SerializeField] GameObject br2;

	[SerializeField] GameObject img1;
	[SerializeField] GameObject img2;
	[SerializeField] GameObject img3;
	[SerializeField] GameObject img4;
	[SerializeField] GameObject img5;
	[SerializeField] GameObject img6;
	[SerializeField] GameObject img7;

	[SerializeField] float rotateSpeed1;
	[SerializeField] float rotateSpeed2;

	void Start () {
		
	}

	void Update () {

		br1.transform.Rotate (0f, 0f, rotateSpeed1);
		br2.transform.Rotate (0f, 0f, -rotateSpeed1);

		img1.transform.Rotate (0f, 0f, -rotateSpeed2);
		img2.transform.Rotate (0f, 0f, rotateSpeed2);
		img3.transform.Rotate (0f, 0f, -rotateSpeed2);
		img4.transform.Rotate (0f, 0f, rotateSpeed2);
		img5.transform.Rotate (0f, 0f, -rotateSpeed2);
		img6.transform.Rotate (0f, 0f, rotateSpeed2);
		img7.transform.Rotate (0f, 0f, -rotateSpeed2);




	}
}
