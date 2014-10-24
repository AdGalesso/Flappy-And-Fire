using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEnemy : MonoBehaviour {

	public static bool newEnemie = true;

	public GameObject enemy;
	public float startIn;
	public float repeatIn;
	public List<GameObject> flags;

	private int instanceCount;
	private List<bool> flaged;


	void Start () {
		flaged = new List<bool> () { false, false };
		InvokeRepeating ("GetEnemy", startIn, repeatIn);
	}

	void Update(){
		if (!flaged[0] && Main.PreviousScore > 0 && Main.PreviousScore == instanceCount) {
			Invoke("GetPreviousFlag", 0);
		}

		if (!flaged[1] && Main.HigherScore > 0 && Main.HigherScore == instanceCount) {
			Invoke("GetRecordFlag", 0);
		}
	}

	void GetEnemy () {
		if (newEnemie) {
			//The enemie start position
			enemy.transform.position = new Vector3 (14f, 0f, 1);
			
			//Create the instance
			Instantiate (enemy);
			
			//Count instances
			instanceCount++;

			//Wins a Fire life
			Main.FireLife += .1f;
		}
	}

	void GetPreviousFlag() {
		flaged [0] = GetFlag (flags [0]);
	}

	void GetRecordFlag() {
		flaged [1] = GetFlag (flags [1]);
	}

	bool GetFlag(GameObject flag)
	{
		Vector3 enemyPosition = enemy.transform.position;

  		flag.transform.position = new Vector3 (
			enemyPosition.x + 1.5f, enemyPosition.y, 1);

		Instantiate (flag);

		return true;
	}

}
