using UnityEngine;
using System.Collections;

public class SingleEnemy : MonoBehaviour {

	private AudioSource[] allAudioSources;

	void OnCollisionEnter2D(Collision2D coll) {

		allAudioSources = (AudioSource[])FindObjectsOfType(typeof(AudioSource));

		foreach (var audio in allAudioSources) {
			audio.Stop();
		}

		this.audio.Play ();

		GetComponent<Animator> ().SetTrigger ("Hit");
	}
}
