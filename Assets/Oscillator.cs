using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
	[SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
	
	[Range(0,1)] float movementFactor; // 1 is full move, 0 is no move
	Vector3 startingPos;
	[Range(0.1f,1)] [SerializeField] float period = 0.5f;

    // Use this for initialization
    void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float cycle = Time.time * period;
		const float tau = Mathf.PI * 2;
		float rawSineWave = Mathf.Sin(cycle * tau);
		movementFactor = rawSineWave / 2f + 0.5f;

		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}

}
