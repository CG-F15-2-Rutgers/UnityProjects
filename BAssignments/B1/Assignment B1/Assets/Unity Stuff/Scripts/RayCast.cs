using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class RayCast : MonoBehaviour {
	List<string> agent_names = new List<string>();
	List<string> movable_objects = new List<string> ();

	public Text AgentCurrentlySelected;
	public Text MovableObjectSelected;
	public Text CameraCurrentlySelected;
	public Text ThingsHappening;

	NavMeshAgent agent;
	private GameObject agentToMove;
	private GameObject movableObject;

	public Transform target;
	public GameObject CameraPosition;
	public GameObject CameraPosition2;
	public GameObject CameraPosition3;
	public GameObject CameraPosition4;
	public GameObject CameraPosition5;
	public GameObject CameraPosition6;
	public GameObject CameraPosition7;

	public float speed = 5;
	public float speed2 = 30;

	private Vector3 offset;
	private Vector3 defaultv;

	// Use this for initialization
	void Start () {

		AgentCurrentlySelected.text = "No agents selected!";
		MovableObjectSelected.text = "No movable object selected!";
		CameraCurrentlySelected.text = "Default Camera selected!";
		ThingsHappening.text = "No key pressed...";

		agentToMove = CameraPosition;
		offset = transform.position - agentToMove.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		//RaycastHit[] hit;

		Debug.DrawRay (ray.origin, ray.direction * 60, Color.cyan);

		/**** 
		This code takes in a left mouse click, and determies of the object
		which was clicked is an agent. If it is an agnent, its name is recorded
		for use. The tag cannot be recorded since it is univeral for all agents.
		Multiple agents for movement can be selected, and will all move to one 
		selected destination.
		 ****/


		/***
		 * If space  bar is pressed, empty list;
		 ***/
		if (Input.GetKeyDown("space")){
			agent_names.RemoveAll(StartsWithAgent);
			ThingsHappening.text = "Space Bar Pressed: All agent removed from list!";
			AgentCurrentlySelected.text = "No Agents Selected";
			Debug.Log ("Space bar pressed, all agents removed from list!");
		}


		/***
		 * If r is pressed, reset level;
		 ***/
		if (Input.GetKeyDown("r")){
			Debug.Log ("R pressed, simlation reset!");
			ThingsHappening.text = "'R' Key pressed, Level Reset!";
			Application.LoadLevel(0);
		}

		/***
		 * Change camera angle for viewing: Press keys 1-7.
		 ***/

		if (Input.GetKeyDown ("1")) {
			Debug.Log ("Camera set to position 1");
			CameraCurrentlySelected.text = "Camera position 1 selected!";
			ThingsHappening.text = "Number Key '1' Pressed";
			agentToMove = CameraPosition;
			
			
		}

		if (Input.GetKeyDown ("2")) {
			Debug.Log ("Camera set to position 2");
			CameraCurrentlySelected.text = "Camera position 2 selected!";
			ThingsHappening.text = "Number Key '2' Pressed";
			agentToMove = CameraPosition2;


		}

		if (Input.GetKeyDown ("3")) {
			Debug.Log ("Camera set to position 3");
			CameraCurrentlySelected.text = "Camera position 3 selected!";
			ThingsHappening.text = "Number Key '3' Pressed";
			agentToMove = CameraPosition3;
			
			
		}

		if (Input.GetKeyDown ("4")) {
			Debug.Log ("Camera set to position 4");
			CameraCurrentlySelected.text = "Camera position 4 selected!";
			ThingsHappening.text = "Number Key '4' Pressed";
			agentToMove = CameraPosition4;
			
			
		}

		if (Input.GetKeyDown ("5")) {
			Debug.Log ("Camera set to position 5");
			CameraCurrentlySelected.text = "Camera position 5 selected!";
			ThingsHappening.text = "Number Key '5' Pressed";
			agentToMove = CameraPosition5;
			
			
		}

		if (Input.GetKeyDown ("6")) {
			Debug.Log ("Camera set to position 6");
			CameraCurrentlySelected.text = "Camera position 6 selected!";
			ThingsHappening.text = "Number Key '6' Pressed";
			agentToMove = CameraPosition6;
			
			
		}

		if (Input.GetKeyDown ("7")) {
			Debug.Log ("Camera set to position 7");
			CameraCurrentlySelected.text = "Camera position 7 selected!";
			ThingsHappening.text = "Number Key '7' Pressed";
			agentToMove = CameraPosition7;
			
			
		}

		/***
		 * If left mouse button is pressed, and agent is pressed, add it to the list;
		 ***/
		if (Input.GetButtonDown ("Fire1")) {
			ThingsHappening.text = "Left Mouse Button selected!";
			if (Physics.Raycast (ray, out hit) == true) {
				if (hit.transform.gameObject.CompareTag("Player")) {

					Debug.Log("--------LEFT CLICK---------");

					string name = hit.collider.gameObject.name;
					agent_names.Add (name);
					Debug.Log ("Count of List:");
					Debug.Log (agent_names[agent_names.Count-1]);
					//printContents(agent_names);
					AgentCurrentlySelected.text = "Agent Selected:" + agent_names[agent_names.Count-1].ToString ();

				}
				if (hit.transform.gameObject.CompareTag("Movable")) {
					Debug.Log ("Movable Object clicked");
					if (movable_objects.Count != 0){
					movable_objects.RemoveAt (0);
					}
					string name_of_movable_object = hit.collider.gameObject.name;
					movable_objects.Add(name_of_movable_object);
					Debug.Log("Count of movable Objects: " + movable_objects[movable_objects.Count-1]);
					MovableObjectSelected.text = "Movable Object Selected:" + movable_objects[0].ToString ();
					
				}

			}
		}


		/**** 
		This code takes in a right mouse click, and determies of the object which was clicked is a surface (ground). If it is a surface, it then measures if any agents were selected.
		If they werent, the camera offset is set to default, which is what the simulations starts in. If agents were selected, a loop goes through the cntents of the list and navigates
		each one through the map.
		 ****/

		if(Input.GetButtonDown ("Fire2") || Input.GetButton ("Fire2")){
			ThingsHappening.text = "Right mouse button pressed!";
			if(Physics.Raycast (ray,out hit) == true){
				if(hit.transform.gameObject.CompareTag ("Ground")){
					Debug.Log("--------RIGHT CLICK---------");



					if (agent_names.Count != null){
						ThingsHappening.text = "Agent currently moving to position!";
						Debug.Log("List of agents not empty! Moving added agents...");
						for(int i = 0; i < agent_names.Count; i ++){
							agentToMove = GameObject.Find(agent_names[i]);
							Debug.Log ("Agent moving successfully. Probably. Maybe.");
							agent = agentToMove.GetComponent<NavMeshAgent>();
							agent.SetDestination (hit.point); 
						}

					}

					if(agent_names.Count == null){
						ThingsHappening.text = "No agents selected to move!";
						Debug.Log("List empty! Select agents to move...");
						agentToMove = CameraPosition;
						 
					}
				}
			}
		}

	}

	void FixedUpdate(){
		if(Input.GetKey(KeyCode.UpArrow)){
			if (movable_objects.Count != 0) {
			movableObject = GameObject.Find (movable_objects[0]);
			movableObject.transform.Translate(Vector3.forward *speed* Time.deltaTime);
			ThingsHappening.text = "Movable object moved up";
			}
			else 
				ThingsHappening.text = "No Object selected to move!";
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			if (movable_objects.Count != 0) {
			movableObject = GameObject.Find (movable_objects[0]);
			movableObject.transform.Translate(-Vector3.forward *speed* Time.deltaTime);
			ThingsHappening.text = "Movable object moved down";
			}
			else 
				ThingsHappening.text = "No Object selected to move!";
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			if (movable_objects.Count != 0) {
			movableObject = GameObject.Find (movable_objects[0]);
			movableObject.transform.Translate(Vector3.left *speed* Time.deltaTime);
			ThingsHappening.text = "Movable object moved left";
			}
			else 
				ThingsHappening.text = "No Object selected to move!";
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			if (movable_objects.Count != 0) {
			movableObject = GameObject.Find (movable_objects[0]);
			movableObject.transform.Translate(Vector3.right *speed* Time.deltaTime);
			ThingsHappening.text = "Movable object moved right";
			}
			else 
				ThingsHappening.text = "No Object selected to move!";
		}

		if(Input.GetKey(KeyCode.A)){
			if (movable_objects.Count != 0) {
			movableObject = GameObject.Find (movable_objects[0]);
			movableObject.transform.Rotate(-Vector3.up *speed2* Time.deltaTime);
			ThingsHappening.text = "Movable object roated left";
			}
			else 
				ThingsHappening.text = "No Object selected to move!";
		}
		if(Input.GetKey(KeyCode.D)){
			if (movable_objects.Count != 0) {
			movableObject = GameObject.Find (movable_objects[0]);
			movableObject.transform.Rotate(Vector3.up *speed2* Time.deltaTime);
			ThingsHappening.text = "Movable object rotated right";
			}
			else 
				ThingsHappening.text = "No Object selected to move!";
		}

		//float moveHorizontal = Input.GetAxis ("Horizontal 2");
		//float moveVertical = Input.GetAxis ("Vertical 2");

	}

	/***
	 * Used for making camera follow agents
	 ***/
	void LateUpdate(){
		transform.position = agentToMove.transform.position + offset;
		}

	/***
	 * Used for printing lists (Debbuging)
	 ***/
	void printContents(List<string> List){
		for (int i = 0; i < List.Count; i++) {
			print(List[i]);
		}
	}

	/***
	 * Used for checking if string starts with agent
	 ***/
	private static bool StartsWithAgent(string s){
		return s.ToLower ().StartsWith ("agent");
		}
	}




