using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TreeSharpPlus;

public class RayCasting : MonoBehaviour {
    NavMeshAgent agent;
    Animator animator;
    bool doorOpen;
    bool doorClose;
    bool doorIdle;

    private GameObject agentToMove;
    private BehaviorAgent behaviorAgent;
    public GameObject COP;
    public GameObject buttonDoor1;
    public GameObject MovingDoor;
    public GameObject CRIMINAL;
    public GameObject HELPME;
    public GameObject buttonDoor1Border;
    public GameObject circle;
    public GameObject circle_around_dude;
    private bool HelpMeselected;
    private Vector3 participantLocation;
    float maxspeed = 10f;

    private Vector3 vector;
    // Use this for initialization
    void Start () {
        doorOpen = false;
        animator = MovingDoor.GetComponent<Animator>();
        participantLocation = HELPME.transform.position;
        HelpMeselected = false;
      
    }
	
	// Update is called once per frame
	void Update () {

        // Debug.Log(COP.transform.position);
        // Debug.Log("HelpMeSelected: " + HelpMeselected);
        // Debug.Log(behaviorAgent.StopBehavior());
        //behaviorAgent.StopBehavior();
       // Debug.Log("Difference in Positions: " + (participantLocation - COP.transform.position).magnitude);


        if (HelpMeselected == true)
        {
            behaviorAgent.StopBehavior();
        }
        


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 60, Color.cyan);

        
        if(Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(ray,out hit) == true)
            {
                if (hit.transform.gameObject.CompareTag("ButtonDoor1"))
                {
                    Debug.Log("~~ ButtonDoor1 Selected ~~");

                    buttonDoor1Border.SetActive(true);
                    Invoke("DontShowButton", 1);
                    string name = hit.collider.gameObject.name;
                    if (doorOpen == false)
                    {
                        Debug.Log("Door Opened");
                        doorOpen = true;
                        Doors("Open");
                    }
                    else if (doorOpen == true)
                    {
                        Debug.Log("Door Closed");
                        doorOpen = false;
                        doorClose = true;
                        Doors("Close");
                    }

                }

                if (hit.transform.gameObject.CompareTag("HelpMe"))
                {
                    vector = hit.point;
                    circle_around_dude.transform.position = vector;
                    circle_around_dude.SetActive(true);
                    Invoke("DontShowCircle2", 1);
                    participantLocation = gameObject.transform.position;
                    // Debug.Log("HelpMeDude" + gameObject.transform.position);
                    //Debug.Log("Help me Dude Clicked");

                }

            }
        }
        
        
        if (Input.GetButtonDown("Fire2") || Input.GetButton("Fire2"))
        {
            if (Physics.Raycast(ray, out hit) == true)
            {
                if(hit.transform.gameObject.CompareTag("Ground"))
                {
                    vector = hit.point;
                    circle.transform.position = vector; 
                    circle.SetActive(true);
                    Invoke("DontShowCircle", 1);
                    agentToMove = GameObject.Find("Cop");
                    agentToMove.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                }

                if (hit.transform.gameObject.CompareTag("HelpMe"))
                {
                    HelpMeselected = true;
                }



            }
        }
        //Debug.Log(Time.deltaTime);
    }

    protected Node ReadWithRightHand(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_BodyAnimation("REACHRIGHT", true), new LeafWait(1000));
    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }

  

    void DontShowButton()
    {
        buttonDoor1Border.SetActive(false);
    }

    void DontShowCircle2()
    {
        circle_around_dude.SetActive(false);
    }

    void DontShowCircle()
    {
        circle.SetActive(false);
    }

    protected Node KickCriminal(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_BodyAnimation("FIGHT", true), new LeafWait(1000));
    }

    
}
