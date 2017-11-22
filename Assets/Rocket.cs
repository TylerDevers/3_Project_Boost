using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audio;
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		ProcessInput();
	}

	private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
		float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        { // can thrsut while rotating
            print("Thursting");
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
		else
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {
		rigidBody.freezeRotation = true; //takes manual control of rotation
		
		float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotate LEft");
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("ROtate Right");
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
		rigidBody.freezeRotation = false; //releases manual control of rotation.
    }

}
