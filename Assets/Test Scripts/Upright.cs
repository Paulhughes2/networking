using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour {

	float uprightTorque;
	RaycastHit fallen;


	// Use this for initialization
	void Start () {
		
		uprightTorque = 100f;

	}
	
	// Update is called once per frame
	void Update () {
		
		if (!Physics.Raycast ( transform.position, -transform.up, out fallen, 1f))
		{
			Debug.DrawRay (transform.position, -transform.up, Color.green);

			var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
 		gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(rot.x, rot.y, rot.z)*uprightTorque);
			
		}else{

			Debug.DrawRay (transform.position, -transform.up, Color.red);

			

		}

		

	}
}
