using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour {

	void OnMouseDown(){
		Main.transitionStartTime = Time.time;
		Main.journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(-20, 0, 0));
		Main.help = true;
		Main.general = false;
	}

}
