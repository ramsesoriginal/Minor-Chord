using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( player.position.x, transform.position.y, transform.position.z);
		var distance = Vector3.Distance (transform.position, new Vector3( transform.position.x, player.position.y, transform.position.z));
		if (distance > 15 && transform.position.y > player.position.y) {
			transform.position = Vector3.Lerp(transform.position, new Vector3( player.position.x, transform.position.y-(distance+15), transform.position.z),0.001f );
		}
		if (distance > 5 && transform.position.y < player.position.y) {
			transform.position = Vector3.Lerp(transform.position, new Vector3( player.position.x, transform.position.y+(distance-5), transform.position.z),0.001f );
		}
	}
}
