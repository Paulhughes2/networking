using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyBehaviour : NetworkBehaviour {

	public float movespeed;

	public Vector3 defaultpos;

	public GameObject target;
	public GameObject[] Players;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	private float HP;

	float stopDistance;
	bool canShoot = true;
	float timer = 0;
	float shootDelay;

	RaycastHit LoS;
	RaycastHit hit;

	// Use this for initialization
	void Start () {

		stopDistance = Random.Range(5,20);
		movespeed = 5;
		target = null;
		defaultpos = new Vector3 (0,0,0);
		shootDelay = 5;
	}
	
	// Update is called once per frame
	void Update () {

		int bulletLayer = (1 >> 8) | (1 << 9);

		RaycastHit LoS;

		Players = GameObject.FindGameObjectsWithTag ("Player");

		HP = gameObject.GetComponent<Health>().currentHealth;

		target = FindClosestPlayer ();
		if (target != null) {



			if (Vector3.Distance (target.transform.position, transform.position) > stopDistance * 5) {
				movespeed = 40;
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}else if (Vector3.Distance (target.transform.position, transform.position) > stopDistance * 4) {
				movespeed =30;
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}else if (Vector3.Distance (target.transform.position, transform.position) > stopDistance * 3) {
				movespeed = 20;
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}else if (Vector3.Distance (target.transform.position, transform.position) > stopDistance * 2) {
				movespeed = 10;
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}else if (Vector3.Distance (target.transform.position, transform.position) > stopDistance) {
				movespeed = 5;
				gameObject.GetComponent<Rigidbody>().useGravity = false;
			}


			if (Vector3.Distance (target.transform.position, transform.position) < stopDistance * 2) {

				if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out LoS, 30.0f, bulletLayer)) {
					
					if (LoS.collider.CompareTag ("Player")) {

						Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * LoS.distance, Color.green);

						if (canShoot) {

							var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

							// Add velocity to the bullet
							bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 20;

							// Spawn the bullet on the Clients
							NetworkServer.Spawn (bullet);

							// Destroy the bullet after 2 seconds
							Destroy (bullet, 2.0f);  

							canShoot = false;
							timer = 0;
						} 
					} else {
						Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * LoS.distance, Color.red);
					}
				}
				if (timer >= shootDelay) {
					canShoot = true;
				}
				timer += (1 * Time.deltaTime);
			}
		}
	}

	void LateUpdate () {

		if (target != null) {

			if (Vector3.Distance(target.transform.position, transform.position) > stopDistance) {

				transform.localPosition = Vector3.MoveTowards (transform.position, target.transform.position, movespeed * Time.deltaTime);
			}
			transform.LookAt (target.transform.position);	
		}
	}

	public GameObject FindClosestPlayer(){

		GameObject closest = null;

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in Players) {

			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;

	}

}