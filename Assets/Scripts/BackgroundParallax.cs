using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {

	public float speed = 0;
	
	void Start () {
	
	}

	void Update () {

		//As the time passes shift the backgroud
		renderer.material.mainTextureOffset = new Vector2 ((Time.time * speed) % 1, 0f);
	}
}
