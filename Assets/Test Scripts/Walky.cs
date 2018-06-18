using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walky : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float vertical = Input.GetAxis ("Vertical") / 3.0f;

		transform.Rotate(0, horizontal, 0);

		transform.Translate(0, 0, vertical);

	}
}
