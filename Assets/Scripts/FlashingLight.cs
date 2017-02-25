using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour {

	Light myLight;
	AnimationCurve curve;
	public float flashInterval;
	public float brightnessLevel;
	float timer;
	// Use this for initialization
	void Start () {
		curve = new AnimationCurve(new Keyframe[] {new Keyframe(0, 1), new Keyframe(flashInterval, brightnessLevel) });
		curve.postWrapMode = WrapMode.PingPong;
		myLight = GetComponent<Light>();	
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetButtonDown("Fire0") || Input.GetButtonDown("Fire1")) && timer > flashInterval * 2) {
			timer = 0;
		}
		timer += Time.deltaTime;
		myLight.intensity = curve.Evaluate(timer);
	}
}
