using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
	[SerializeField] Vector3 movementVector;
	[Range(0,1)] [SerializeField] float movementFactor; // 1 is full move, 0 is no move
	Vector3 startingPos;
	
	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}

}
