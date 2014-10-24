using UnityEngine;
using System.Collections;

public class IdleMove : MonoBehaviour {

	private bool isBusy = false;

	void Update () {
		if (!isBusy) {
			StartCoroutine ("GetSingleFlappy");	
		}

	}

	IEnumerator GetSingleFlappy()
	{
		isBusy = true;
		yield return new WaitForSeconds(2);
		GetComponent<Animator> ().SetTrigger ("Flappy");
		isBusy = false;
	}
}
