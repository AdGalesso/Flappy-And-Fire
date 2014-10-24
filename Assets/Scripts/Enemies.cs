using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {
	
	public Vector2 speed = new Vector2(-4, 0);
	public List<float> randYPosition;

	void Start () {
		//Speed from right to left <-
		rigidbody2D.velocity = speed;

		//Y random postion
		transform.position = new Vector3 (transform.position.x, Random.Range(randYPosition[0], randYPosition[1]), 1);
	}

	void OnBecameInvisible() {
		Die();
	}

	void Die ()
	{
		Destroy (this.gameObject, 2f);
	}
}
