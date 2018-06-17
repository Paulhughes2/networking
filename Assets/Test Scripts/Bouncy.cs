using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour {

	public float MinDistance = 2.0f;
	public float MaxDistance = 5.0f;
	public float MaxForce = 15.0f;


	float RaycastDownwardsFromMe()
	{
		RaycastHit rch;
		if (Physics.Raycast ( transform.position, Vector3.down, out rch, MaxDistance))
		{
			Debug.DrawRay (transform.position, Vector3.down * rch.distance, Color.green);
			return rch.distance;
		}
		return 100;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){

		float distance = RaycastDownwardsFromMe();
		float fractionalPosition = (MaxDistance - distance) / (MaxDistance - MinDistance);
		if (fractionalPosition < 0) fractionalPosition = 0;
		if (fractionalPosition > 1) fractionalPosition = 1;
		float force = fractionalPosition * MaxForce;
		gameObject.GetComponent<Rigidbody>().AddForce( Vector3.up * force);

	}
}
