using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
	public Controls player;

	
	void OnTriggerEnter (Collider other) {
		player.isGrounded = true;
	}

	void OnTriggerStay (Collider other) {
		Debug.DrawRay(other.transform.position, Vector3.up*100, Color.white);
		player.isGrounded = true;
	}

	void OnTriggerExit (Collider other) {
		player.isGrounded = false;
	}

}
