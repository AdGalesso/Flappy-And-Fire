using UnityEngine;
using System.Collections;

public class ToArray : MonoBehaviour {
	
	void Awake () {
		Invoke ("NextScene", 3);
	}

	void NextScene(){
		Application.LoadLevel ("Menu");
	}
}
