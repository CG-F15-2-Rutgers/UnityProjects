using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TreeSharpPlus;

public class RayCasting : MonoBehaviour {
    NavMeshAgent agent;
    private GameObject agentToMove;
    private BehaviorAgent behaviorAgent;
    public GameObject COP;
    public GameObject buttonDoor1;
    public GameObject CRIMINAL;
    public GameObject buttonDoor1Border;
    public GameObject circle;
    public GameObject circle_around_dude;

    private Vector3 vector;
    // Use this for initialization
    void Start () {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }
	
	// Update is called once per frame
	void Update () {
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
                    Invoke("DontShowButton",1);

                    string name = hit.collider.gameObject.name;
                }

                if (hit.transform.gameObject.CompareTag("HelpMe"))
                {
                    vector = hit.point;
                    circle_around_dude.transform.position = vector;
                    circle_around_dude.SetActive(true);
                    Invoke("DontShowCircle2", 1);
                    Debug.Log("Help me Dude Clicked");

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

                

            }
        }
        
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


    protected Node GunIdle(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        
            return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_HandAnimation("PISTOLAIM", true), new LeafWait(1000));
       
    }

    protected Node BuildTreeRoot()
    {

            return new DecoratorLoop( 
            new Sequence(this.GunIdle(this.COP.transform)));
            
        
    }
}
