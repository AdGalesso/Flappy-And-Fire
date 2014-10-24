using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
	
	void OnMouseDown () {
		Main.transitionStartTime = Time.time;
		Main.journeyLength = Vector3.Distance(Camera.allCameras[0].transform.position, new Vector3(0, 10, 0));
		Main.opt = true;
		Main.general = false;
	}
}
