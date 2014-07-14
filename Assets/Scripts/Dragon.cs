using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public Vector2 jump;

	private float lastTap;
	private float timeTap;
	
	void Start () {
		timeTap = .2f;
	}

	void Update () {

		//Fixed rotation and x position
		transform.rotation = Quaternion.Euler(0f, 0f, 350f);
		transform.position = new Vector3 (-5.5f, transform.position.y, transform.position.z);

		if (Input.GetKeyDown(KeyCode.Space)) {

			SingleTap();

			if ((Time.time - lastTap) < timeTap) {
				Debug.Log("DoubleTap" + Time.time);
			}

			lastTap = Time.time;
		}

	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Floor") {
			SingleTap();
		}
		else if(coll.collider.tag == "Ceiling") {
			SingleTap(true);
		}
		else if (coll.collider.tag != "Point") {
			Die (coll.collider.tag);
		}
	}

	void SingleTap (bool negative = false)
	{
		GetComponent<Animator> ().SetTrigger ("Flappy");

		rigidbody2D.AddForce(negative ? -jump : jump);

		audio.Play();

		//Wins a Fire life
		Main.FireLife += 0.21f;
	}
	
	void Die (string killer)
	{
		//Call Animation Kill
		if (killer == "Tree") {
			GetComponent<Animator> ().SetBool ("DeathAngel", true);
		} 
		else {
			GetComponent<Animator> ().SetBool ("DeathBurn", true);
		}

		//Transform the object
		this.rigidbody2D.isKinematic = true;
		CreateEnemy.newEnemie = false;

		//Score
		if (Main.Score > Main.HigherScore) {
			PlayerPrefs.SetInt("HigherScore", Main.Score);
		}

		PlayerPrefs.SetInt("PreviousScore", Main.Score);

		//Call Die Menu
		Main.DieMenu = true;

		//Kill ´D´
		Destroy (this.gameObject, 3);
	}
}
