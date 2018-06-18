using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpy : MonoBehaviour {

	float moveSpeed;
	Vector3 startPos;
	float jumpHeight;
	RaycastHit toFloor;
	public float maxDistance = 0.5f;
	public float dist;
	Vector3 pos;
	float uprightTorque;
	RaycastHit fallen;

	bool jumping;
	bool canJump;
	bool sticky;

	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		moveSpeed = 7f;
		jumpHeight= 5f;
		jumping = false;
		canJump = true;
		uprightTorque = 100f;
		sticky = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space) && (canJump == true)) 
		{
			jumping = true;
			canJump = false;	
			sticky = false;
		}
		if(jumping == true){

			//freeze rotation

			pos = transform.localPosition;

			pos.y += jumpHeight;
			
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, moveSpeed * Time.deltaTime);
		}
		
		if (Physics.Raycast ( transform.position, Vector3.down, out toFloor, 30f))
		{
			Debug.DrawRay (transform.position, Vector3.down * toFloor.distance, Color.green);
			
		}
		 dist = Vector3.Distance(toFloor.point, transform.position);
		if (dist < 0.6f){
			jumping = false;
        	canJump = true;
        	sticky = true;
		}

		if((toFloor.distance > maxDistance) && sticky ==true){
			transform.localPosition = new Vector3(transform.localPosition.x, (toFloor.point.y + maxDistance),transform.localPosition.z);
			
		}
		if (jumping==false){

			if (!Physics.Raycast ( transform.position, -transform.up, out fallen, 1f))
		{
			Debug.DrawRay (transform.position, -transform.up, Color.green);

			var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
 		gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(rot.x, rot.y, rot.z)*uprightTorque);
			
		}
		}	
		
	}
}
