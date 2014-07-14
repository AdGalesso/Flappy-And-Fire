using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (CreateEnemy.newEnemie) {
			Main.Score += 1;
			Destroy (this.gameObject);
		}
	}
}
