using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput();
	}

	private void ProcessInput() {
		if (Input.GetKey(KeyCode.Space)) { // can thrsut while rotating
			print("Thursting");
			rigidBody.AddRelativeForce(Vector3.up);
			if (!audio.isPlaying) {
				audio.Play();
			}
		} else if (Input.GetKeyUp(KeyCode.Space)) {
				audio.Stop();
		}
		if (Input.GetKey(KeyCode.A)){
			print("Rotate LEft");
			transform.Rotate(Vector3.forward);
		} else if (Input.GetKey(KeyCode.D)) {
			print("ROtate Right");
			transform.Rotate(-Vector3.forward);
		}
	}

}

