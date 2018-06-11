using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	void OnEnable() {
		CameraPreRender.onPreCull += MyPreCull;
	}

	void OnDisable() {
		CameraPreRender.onPreCull -= MyPreCull;
	}

	void MyPreCull() {
		//we want to look back
		Vector3 difference = Camera.current.transform.position - transform.position;
		transform.LookAt(transform.position - difference, Camera.current.transform.up);
	}
}