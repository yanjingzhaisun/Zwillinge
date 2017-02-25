using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2" )){
			SceneManager.LoadScene("tutorial");
		}
		if(Input.GetButtonDown("Fire3" )){
			Debug.Log ("fire3");
			SceneManager.LoadScene("tutorial");
		}
	}
}
