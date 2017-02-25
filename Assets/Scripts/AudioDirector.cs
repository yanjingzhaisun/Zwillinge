using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class AudioDirector : MonoBehaviour {
	public static AudioDirector instance;
	public List<AudioMixerSnapshot> snapshots;
	public AudioSource final;
	public AudioSource reset;
	public AudioSource movewall;
	List<AudioSource> SFXs;
	List<AudioSource> resets;

	void Awake() {
		if (instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	public void PlaySnapshots(int stages) {
		stages = Mathf.Clamp(stages, 0, snapshots.Count - 1);
		snapshots[stages].TransitionTo(0.5f);
	}

	void Start() {
		resets = new List<AudioSource>();
		resets.Add(reset);
		SFXs = transform.Find("SFX").GetComponentsInChildren<AudioSource>().ToList();
		PlaySnapshots(2);
	}

	void Update() {
#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.LeftShift)) {
			if (Input.GetKeyUp(KeyCode.Alpha0))
				PlaySnapshots(0);
			else if (Input.GetKeyUp(KeyCode.Alpha1))
				PlaySnapshots(1);
			else if (Input.GetKeyUp(KeyCode.Alpha2))
				PlaySnapshots(2);
			else if (Input.GetKeyUp(KeyCode.Alpha3))
				PlaySnapshots(3);
			else if (Input.GetKeyUp(KeyCode.Alpha4))
				PlaySnapshots(4);
			else if (Input.GetKeyUp(KeyCode.Alpha5))
				PlaySnapshots(5);
			else if (Input.GetKeyUp(KeyCode.Alpha6))
				PlayFinal();
		}
		#endif
	}

	public void PlaySFX(int NoteNumber) {
		NoteNumber = Mathf.Clamp(NoteNumber, 0, SFXs.Count - 1);
		SFXs[NoteNumber].Play();
	}
	public void PlaySFX() {
		int noteNumber = Random.Range(0, SFXs.Count);
		PlaySFX(noteNumber);
	}

	public void PlayFinal() {
		final.Play();
	}

	public void PlayReset() {
		AudioSource resetsound = resets.FirstOrDefault(p => !p.isPlaying);
		if (resets.Count < 10 && resetsound == null)
		{
			resetsound = Instantiate<AudioSource>(reset, transform);
			resetsound.pitch = Random.Range(-3, 4);
			resetsound.Play();
			resets.Add(resetsound);
		}
		else if (resetsound != null)
			resetsound.Play();

	}

	public void PlayMoveWall() {
		movewall.Play();
	}
}
