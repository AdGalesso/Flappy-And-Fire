using System;
using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public static int Score;
	public static int HigherScore;
	public static int PreviousScore;
	public static bool DieMenu;
	public static float FireLife;

	public Vector2 pos;
	public Vector2 size;
	
	public GUISkin GlobalGUI;
	public bool Menu;
	public Texture progressBarEmpty;
	public Texture progressBarFull;
	public Texture forkme;

	public static float AverageScreenX = Screen.width / 2;
	public static float AverageScreenY = Screen.height / 2;
	public static float Width = (AverageScreenX / 1);
	public static float Height = (AverageScreenY / 3.6f);
	public static float Left = AverageScreenX - (Width) / 2;
	public static int TitleSize = Screen.width / 14;
	public static int LabelSize = Screen.width / 20;
	public static int ButtonSize = Screen.width / 16;

	public static float speed = 10;
	public static float transitionStartTime = 0;
	public static float journeyLength;

	public static bool opt = false;
	public static bool help = false;
	public static bool general = false;

	void Start () {
		Score = 0;
		FireLife = 0;
		HigherScore = PlayerPrefs.GetInt ("HigherScore");
		PreviousScore = PlayerPrefs.GetInt ("PreviousScore");

		//FB.Init(OnInitComplete, OnHideUnity);
	}

	void FixedUpdate()
	{
		if (opt) {
			float distance = (Time.time - transitionStartTime) * speed;
			float fracJourney = distance / journeyLength;
			Camera.allCameras[0].transform.position = Vector3.Lerp(Vector3.zero, new Vector3(0, 10, 0), fracJourney);
		}

		if (help) {
			float distance = (Time.time - transitionStartTime) * speed;
			float fracJourney = distance / journeyLength;
			Camera.allCameras[0].transform.position = Vector3.Lerp(Vector3.zero, new Vector3(-20, 0, 0), fracJourney);
		}

		if (general) {
			float distance = (Time.time - transitionStartTime) * speed;
			float fracJourney = distance / journeyLength;
			Camera.allCameras[0].transform.position = Vector3.Lerp(Camera.allCameras[0].transform.position, new Vector3(0, 0, 0), fracJourney);		
		}
	}

	void OnGUI() {
		GUI.skin = GlobalGUI;

		GlobalGUI.label.fontSize = Main.LabelSize;
		GlobalGUI.button.fontSize = Main.ButtonSize;

		if (DieMenu) {
			GetDieMenu();
		} 
		else {
			if (Menu) {
				GetDefaultMenu();
			} 
			else {
				GetFireBar();
			}
		}
	}	

	void GetDefaultMenu ()
	{
		if (GUI.Button (new Rect (Screen.width - forkme.width - 30, -1, forkme.width, forkme.height), forkme)) {
			Application.OpenURL ("https://github.com/AdGalesso/Flappy-And-Fire");
		}

		GUIStyle style = new GUIStyle();
		
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = Color.white;

		if (help) {
			if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 50, 100, 50), "Back to Menu", style)) {

				transitionStartTime = Time.time;
				journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(0, 0, 0));

				help = false;
				general = true;
			}
		}

		if (opt) {

			//GUI.Button (new Rect (Screen.width / 2 + 250, Screen.height - 500, 100, 50), FB.IsLoggedIn.ToString(), style);
			
			//if (!FB.IsLoggedIn)
			//{
				//if (GUI.Button (new Rect (Screen.width / 2 + 250, Screen.height - 300, 100, 50), "Facebook", style)) {
					
					//FB.Login("email,publish_actions", LoginCallback);
				//}
			//}

			if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 50, 100, 50), "Back to Menu", style)) {
				
				transitionStartTime = Time.time;
				journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(0, 0, 0));
				
				opt = false;
				general = true;
			}
		}
	}

	void GetFireBar ()
	{
		GUI.Label (new Rect (10, 10, 1000, 100), string.Format (@"Your Points: {0}", Score.ToString ()));
		
		GUI.BeginGroup(new Rect(pos.x, Screen.height-55, size.x, size.y));
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

	void GetDieMenu ()
	{
		GUI.BeginGroup (new Rect (80, 70, Screen.width - 200, Screen.height - 80));
		{
			GUIStyle style = new GUIStyle();
			
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = Main.ButtonSize;
			style.normal.textColor = Color.Lerp(Color.yellow, Color.red, 0.5f);
			
			GUI.Box (new Rect (0, 0, Screen.width - 200, Screen.height - 80), string.Format (@"Your Points: {0}", Score.ToString ()), style);
			
			style.normal.textColor = Color.Lerp(Color.green, Color.black, 0.5f);
			
			GUI.Box (new Rect (0, 100, Screen.width - 200, Screen.height - 80), string.Format (@"Your Best: {0}", HigherScore.ToString ()), style);

			if (GUI.Button (new Rect (0, 200, Screen.width - 200, 80), "Try Again")) {
				FireLife = 0;
				DieMenu = Menu = false;
				CreateEnemy.newEnemie = true;
				Application.LoadLevel ("Game");
			}
			
			if (GUI.Button (new Rect (0, 300, Screen.width - 200, 80), "Back to Menu")) {
				DieMenu = false;
				Menu = true;
				CreateEnemy.newEnemie = true;
				Application.LoadLevel ("Menu");
			}
		}
		GUI.EndGroup ();
	}

	void OnInitComplete ()
	{
		Debug.Log("Init");
	}

	void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}

	void LoginCallback (FBResult result)
	{
		// Reqest player info and profile picture
		FB.API("/me?fields=id,email,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);

		//LoadPicture(Util.GetPictureURL("me", 128, 128),MyPictureCallback);
	}

	void APICallback (FBResult result)
	{
		Debug.Log (result.Text);

		FB.API("/me/permissions", Facebook.HttpMethod.GET, delegate (FBResult response) {
			Debug.Log(response.Text);
			// inspect the response and adapt your UI as appropriate
			// check response.Text and response.Error
		});

		if (result.Error != null) {


			Dictionary<string,string> profile = Util.DeserializeJSONProfile(result.Text);
			//GameStateManager.Username = profile["first_name"];
			//friends = Util.DeserializeJSONFriends(result.Text);
			//checkIfUserDataReady();

			Debug.Log(profile);
		}

		//Debug.Log("a");
	}
}
