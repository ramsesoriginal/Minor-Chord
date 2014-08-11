using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	private bool running = true;

	void Start () {
		running = true;
	}

	void OnTriggerEnter (Collider other) {
		running = false;
	}

	void OnGUI() {
		if (!running) {
			GUIStyle style = new GUIStyle ();
			style.normal.textColor = Color.white;
			style.fontSize = 100;
			GUI.Label (new Rect (10, 10, 400, 200), "You WON!", style);
			Time.timeScale = 0;
		}
	}
}
