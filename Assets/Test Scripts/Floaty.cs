using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour {

	public float maxDistance = 0.5f;
	RaycastHit toFloor;

		// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
		if (Physics.Raycast ( transform.position, Vector3.down, out toFloor, 30f))
		{
			Debug.DrawRay (transform.position, Vector3.down * toFloor.distance, Color.green);
			
		}
		
		if(toFloor.distance > maxDistance){
			transform.localPosition = new Vector3(transform.localPosition.x, (toFloor.point.y + maxDistance),transform.localPosition.z);
		}
	}
}
