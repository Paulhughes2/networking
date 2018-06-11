using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{

private Vector3 ResetCameraPos;
private Vector3 ResetCameraRot;

private Vector3 dragOrigin;
private bool isPanning;

private float MaxClamp = 80;
private float MinClamp = 60;
public float speedscalar = 100f;
public float number;
private float Scroll;
private bool resetting = false;

public Transform target;
public GameObject player;
public Camera cam;

private Vector3 Relmove;
private Vector3 CurrentPos;
private float CamMove;

void Start () {
		ResetCameraPos = target.localPosition;
		ResetCameraRot = target.rotation.eulerAngles;
		Debug.Log (ResetCameraRot);
		transform.LookAt (target);
		CamMove = 5;
}

void Update()
	{
		CurrentPos = target.localPosition;
		cam.fieldOfView -= 10 * Input.GetAxis("Mouse ScrollWheel");
		cam.fieldOfView = Mathf.Clamp (cam.fieldOfView, MinClamp, MaxClamp);

		if (Input.GetKeyDown (KeyCode.Home)) {
			target.transform.position = ResetCameraPos;
			target.transform.Rotate(ResetCameraRot);
			Debug.Log ("RESET?");
		}

		//Zoom

		//cam.transform.position += cam.transform.forward * Scroll;
			//cam.transform.position += cam.transform.forward * Scroll;

		//movement
		if (Input.GetMouseButtonDown (1)) 
		{
			//right click was pressed    

			dragOrigin = Input.mousePosition;
			isPanning = true;
			Debug.Log("endclick happens" + CurrentPos);
		}
		// cancel on button release
		if (Input.GetMouseButtonUp (1)) 
		{
			isPanning = false;
			resetting = true;
			//target.transform.localPosition = new Vector3 (0, 0 ,7);
			transform.LookAt (target);
		}
		if (resetting) 
		{
			if (CurrentPos != ResetCameraPos) {
				target.transform.localPosition = Vector3.MoveTowards (CurrentPos, ResetCameraPos, CamMove * Time.deltaTime);
				Debug.Log ("Resetting");
				transform.LookAt (target);
			} else {
				resetting = false;

			}
		}
		//move camera on X
		if (isPanning) 
		{
			Relmove = ((Input.mousePosition) - dragOrigin);
			Vector3 NewPos = CurrentPos + Relmove;

			Debug.Log ("updatePos" + CurrentPos + " + " + Relmove + " = " + NewPos);
			if (Relmove.x > 0 || Relmove.x < 0) 
			{
				Debug.Log ("passed Rel check");
				Debug.Log("Moving??");
				target.transform.localPosition = new Vector3 (Mathf.Clamp (NewPos.x / speedscalar , -6f, 6f), 0, 7.25f);
				transform.LookAt (target);
			}
		}

	}

}