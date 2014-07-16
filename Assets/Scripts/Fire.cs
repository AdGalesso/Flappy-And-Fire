using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Tree") {
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
