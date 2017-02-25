using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {


	public float rotationSpeed;

	public GameObject[] p1Images;
	public GameObject[] p2Images;
	public GameObject[] neutralImages;

	private GameObject[] currentImages;

	void Start() {
		
		for(int i = 0; i < p1Images.Length; i++) {
				p1Images[i] = Instantiate(p1Images[i], transform.position, Quaternion.identity);
				p1Images[i].transform.parent = transform;
				p1Images[i].transform.localPosition = Vector3.zero;
				p1Images[i].SetActive(false);
			}
			for(int i = 0; i < p2Images.Length; i++) {
				p2Images[i] = Instantiate(p2Images[i], transform.position, Quaternion.identity);
				p2Images[i].transform.parent = transform;
				p2Images[i].transform.localPosition = Vector3.zero;
				p2Images[i].SetActive(false);
			}
			for(int i = 0; i < neutralImages.Length; i++) {
				neutralImages[i] = Instantiate(neutralImages[i], transform.position, Quaternion.identity);
				neutralImages[i].transform.parent = transform;
				neutralImages[i].transform.localPosition = Vector3.zero;
			}
		currentImages = neutralImages;
	}
	
	// Update is called once per frame
	void Update () {
		currentImages[0].transform.Rotate(0, 0, rotationSpeed);
		currentImages[1].transform.Rotate(0, 0, -rotationSpeed);
		currentImages[2].transform.Rotate(0, 0, rotationSpeed);
		//transform.Rotate(0, 0, rotationSpeed);
	}

	void EnableImages() {
			for(int i = 0; i < p1Images.Length; i++) {
				p1Images[i].SetActive(rotationSpeed > 0 ? true : false);
			}
			for(int i = 0; i < p2Images.Length; i++) {
				p2Images[i].SetActive(rotationSpeed < 0 ? true : false);
			}
			for(int i = 0; i < neutralImages.Length; i++) {
				neutralImages[i].SetActive(rotationSpeed == 0 ? true : false);
			}
	}

	public void ChangeRotation(float newRotation) {
		rotationSpeed = newRotation;
		if(newRotation > 0.1f) {
			currentImages = p1Images;
		}
		else if(newRotation < -0.1f) {
			currentImages = p2Images;
		}
		else {
			currentImages = neutralImages;
		}
		EnableImages();
	}
}
