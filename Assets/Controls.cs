using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public float aceleration;
	public float speed;
	public float maxSpeed;
	public float airDampening;
	public float jumpForce;
	public int multiJumpCharges;
	public int currentMultiJumpCharges;
	public int jumpDuration;
	public int currentJumpDuration;
	public bool isGrounded;

	private bool jumpJustReleased;

	void Start() {
		Screen.lockCursor = true;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
		var h = Mathf.Abs(Input.GetAxis ("Horizontal"));
		var v = Mathf.Abs(Input.GetAxis ("Vertical"));
		var jump = Input.GetButton("Jump");
		var rtl = Input.GetAxis ("Horizontal") < -0.1;
		var ltr = Input.GetAxis ("Horizontal") > 0.1;
		speed = Mathf.Abs(rigidbody.velocity.x);
		if (speed < maxSpeed) {
			if (isGrounded) {
				rigidbody.AddForce (transform.forward * h * aceleration);
			} else {
				rigidbody.AddForce (transform.forward * h * aceleration * airDampening);
			}
		}
		if (isGrounded) {
			currentMultiJumpCharges = multiJumpCharges + 1 ;
			currentJumpDuration = jumpDuration+1;
		}
		if (jump && currentJumpDuration > 0) {
			rigidbody.AddForce (transform.up * jumpForce);
			currentJumpDuration--;
			jumpJustReleased = true;
		}
		if (jumpJustReleased && !jump) {
			jumpJustReleased = false;
			if (currentMultiJumpCharges > 0) {
				currentJumpDuration = jumpDuration+1;
			}
			currentMultiJumpCharges--;
		}
		if (rtl) {
			transform.rotation = Quaternion.Euler(0,-90,0);//new Quaternion(0,-90,0,1);
		}
		if (ltr) {
			transform.rotation = Quaternion.Euler(0,90,0);
		}
	}
}
