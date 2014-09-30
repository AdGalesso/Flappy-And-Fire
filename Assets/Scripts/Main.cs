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
	
	private float speed = 10;
	private float transitionStartTime = 0;
	private float journeyLength;

	private bool opt = false;
	private bool help = false;
	private bool general = false;

	void Start () {
		Score = 0;
		FireLife = 0;
		HigherScore = PlayerPrefs.GetInt ("HigherScore");
		PreviousScore = PlayerPrefs.GetInt ("PreviousScore");

		FB.Init(OnInitComplete, OnHideUnity);
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
		if (GUI.Button (new Rect (Screen.width - 300, -1, 64, 128), forkme)) {
			Application.OpenURL ("https://github.com/AdGalesso/Flappy-And-Fire");
		}
		
		if (!opt && !help) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 10, 200, 50), string.Empty)) {
				Application.LoadLevel ("Game");
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 160, Screen.height / 2 + 60, 200, 60), string.Empty)) {
				transitionStartTime = Time.time;
				journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(0, 10, 0));
				opt = true;
				general = false;
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 130, Screen.height / 2 + 120, 200, 70), string.Empty)) {
				transitionStartTime = Time.time;
				journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(-20, 0, 0));
				help = true;
				general = false;
			}
		}

		GUIStyle style = new GUIStyle();
		
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = 21;
		style.normal.textColor = Color.white;

		if (help) {
			if (GUI.Button (new Rect (Screen.width / 2 + 250, Screen.height - 100, 100, 50), "Back to Menu", style)) {

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

			if (GUI.Button (new Rect (Screen.width / 2 + 250, Screen.height - 100, 100, 50), "Back to Menu", style)) {
				
				transitionStartTime = Time.time;
				journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(0, 0, 0));
				
				opt = false;
				general = true;
			}
		}
	}

	void GetFireBar ()
	{
		GUI.Label (new Rect (10, 10, 300, 100), string.Format (@"Your Points: {0}", Score.ToString ()));
		
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
			style.fontSize = 40;
			style.normal.textColor = Color.Lerp(Color.yellow, Color.red, 0.5f);
			
			GUI.Box (new Rect (0, 0, Screen.width - 200, Screen.height - 80), string.Format (@"Your Points: {0}", Score.ToString ()), style);
			
			style.normal.textColor = Color.Lerp(Color.green, Color.black, 0.5f);
			
			GUI.Box (new Rect (0, 60, Screen.width - 200, Screen.height - 80), string.Format (@"Your Best: {0}", HigherScore.ToString ()), style);

			style.fontSize = 21;
			style.normal.textColor = Color.white;

			if (GUI.Button (new Rect (10, 160, Screen.width - 220, 40), "Try Again", style)) {
				FireLife = 0;
				DieMenu = Menu = false;
				CreateEnemy.newEnemie = true;
				Application.LoadLevel ("Game");
			}
			
			if (GUI.Button (new Rect (10, 200, Screen.width - 220, 40), "Back to Menu", style)) {
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
