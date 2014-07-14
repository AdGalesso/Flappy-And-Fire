using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public static int Score;
	public static int HigherScore;
	public static int PreviousScore;
	public static bool DieMenu;
	public static float FireLife = 0;

	public Vector2 pos = new Vector2(30,40);
	public Vector2 size = new Vector2(2000,2000);
	
	public GUISkin GlobalGUI;
	public bool Menu;
	public Texture progressBarEmpty;
	public Texture progressBarFull;

	void Start () {
		Score = 0;
		HigherScore = PlayerPrefs.GetInt ("HigherScore");
		PreviousScore = PlayerPrefs.GetInt ("PreviousScore");
	}

	void OnGUI() {
		GUI.skin = GlobalGUI;

		if (DieMenu) {
			GUI.BeginGroup (new Rect (80, 70, Screen.width - 200, Screen.height - 80));
			{
				GUI.Box (new Rect (0, 0, Screen.width - 200, Screen.height - 80), Score.ToString ());

				if (GUI.Button (new Rect (10, 50, Screen.width - 220, 40), "Try Again")) {
					DieMenu = Menu = false;
					CreateEnemy.newEnemie = true;
					Application.LoadLevel ("Game");
				}

				if (GUI.Button (new Rect (10, 100, Screen.width - 220, 40), "Back to Menu")) {
					DieMenu = false;
					Menu = true;
					CreateEnemy.newEnemie = true;
					Application.LoadLevel ("Menu");
				}
			}
			GUI.EndGroup ();
		} 
		else {
			if (Menu) {
				if (GUI.Button (new Rect (Screen.width - 150, 0, 150, 150), string.Empty)) {
					Application.OpenURL ("https://github.com/AdGalesso/Teste");
				}

				if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 10, 200, 50), string.Empty)) {
					Application.LoadLevel ("Game");
				}

				if (GUI.Button (new Rect (Screen.width / 2 - 120, Screen.height / 2 + 60, 200, 60), string.Empty)) {
					Application.LoadLevel ("Game");
				}

				if (GUI.Button (new Rect (Screen.width / 2 - 110, Screen.height / 2 + 135, 200, 50), string.Empty)) {
					Application.LoadLevel ("Game");
				}
			} 
			else {
				GUI.Label (new Rect (10, 10, 300, 100), string.Format (@"Your Points: {0}", Score.ToString ()));

				GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
				{
					GUI.Box(new Rect(0,0, size.x, size.y), progressBarEmpty);

					GUI.BeginGroup(new Rect (0, (size.y - (size.y  * FireLife)), size.x, size.y  * FireLife));
					{
						GUI.Box(new Rect (0, -size.y + (size.y * FireLife), size.x, size.y), progressBarFull);
					}
					GUI.EndGroup();
				}
				GUI.EndGroup();
			}
		}
	}	
}
