using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public Camera camera;
	public Rigidbody rb;

	float speed = 10f;
	float horizontal;
	float slowModifier = 0.9f;

	Vector3 currentVel;
	Vector3 Eulervelo;

	RaycastHit floaty;

	void Start(){
		
	
	}

	void Update()
	{

	


		if (!isLocalPlayer)
		{
			camera.enabled = false;
			return;
		}

		// Grabs horizonal movement from "A" + "D"
		horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;

		// Stores rotation * speed as Euler
		Eulervelo = new Vector3 (0, horizontal * speed, 0); 

		// OLD MOVEMENT

		/*
		float horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float vertical = Input.GetAxis ("Vertical");

		transform.Rotate(0, horizontal, 0);

		transform.Translate(0, 0, vertical);
		*/

	
	}

	void FixedUpdate(){
		
		// Quaternion of the Euler rotation * time
		Quaternion CharRotation = Quaternion.Euler (Eulervelo * Time.deltaTime);

		// Add velocity on "W" press
		if (Input.GetKey(KeyCode.W)) 
		{
			rb.velocity = transform.forward *speed;
		}
		// lower velocity times  modifier on "S" press
		if (Input.GetKey(KeyCode.S)) 
		{
			rb.velocity = rb.velocity * slowModifier;
		}
		// Rotation
		if (Input.GetKey(KeyCode.A)) 
		{
			//Grab current velocity and store it
			currentVel = rb.velocity;

			// if velocity is 0 rotate on the spot
			if (currentVel == Vector3.zero) {
				rb.MoveRotation (rb.rotation * CharRotation);
			}else{
			// if velocity is not 0, make it 0
				rb.velocity = Vector3.zero;

			/*	Rotate the rigidbody by earlier Quaternion
				move rotation is smooth rotate.
				gets current rotation * how muhc you want to rotate
				works in steps not jumps 
			*/	
				rb.MoveRotation (rb.rotation * CharRotation);

			// set velocity to new forward * the magnitude it started at
				rb.velocity = transform.forward * currentVel.magnitude;
			}
		}
		if (Input.GetKey(KeyCode.D)) 
		{
			//Grab current velocity and store it
			currentVel = rb.velocity;

			// if velocity is 0 rotate on the spot
			if (currentVel == Vector3.zero) {
				rb.MoveRotation (rb.rotation * CharRotation);
			}else{
			// if velocity is not 0, make it 0
				rb.velocity = Vector3.zero;

			/*	Rotate the rigidbody by earlier Quaternion
				move rotation is smooth rotate.
				gets current rotation * how muhc you want to rotate
				works in steps not jumps 
			*/	
				rb.MoveRotation (rb.rotation * CharRotation);

				// set velocity to new forward * the magnitude it started at
				rb.velocity = transform.forward * currentVel.magnitude;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}

	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);        
	}

	public override void OnStartLocalPlayer ()
	{
		GetComponent<Renderer>().material.color = Color.blue;
	}
}