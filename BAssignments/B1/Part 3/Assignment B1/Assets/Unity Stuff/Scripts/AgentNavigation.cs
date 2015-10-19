using UnityEngine;
using System.Collections;

public class AgentNavigation : MonoBehaviour {
	public Camera camera_to_be_used;
	public bool zooming;
	public float zoomSpeed;

	//Camera Camera_to_be_used = GetComponent<Camera>();

	// Use this for initialization
	void Start () {
	

		/*
		// Selecting objects with mouse and moving them to target position (right click)
		RaycastHit hit;
		Ray ray = camera_to_be_used.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			Transform objectHit = hit.transform;
			// Do something with the object that was hit by the raycast; 
			print ("Hit by Ray Cast");
		}
*/


	}
	
	// Update is called once per frame
	void Update () {
		/*
		// Zooming in on objects selected
		if (zooming) {
			Ray ray = camera_to_be_used.ScreenPointToRay (Input.mousePosition);
			float zoomDistance = zoomSpeed * Input.GetAxis ("Vertical") * Time.deltaTime;
			camera_to_be_used.transform.Translate (ray.direction * zoomDistance, Space.World);
		}

		if (Input.GetButton ("Fire1")) {
			print ("Detected Mouse Click");
		}
		*/


	}
}
