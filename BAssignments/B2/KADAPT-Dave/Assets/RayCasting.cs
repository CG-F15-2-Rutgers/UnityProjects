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
    public GameObject CRIMINAL;

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

        /*
        if(Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(ray,out hit) == true)
            {
                if (hit.transform.gameObject.CompareTag("Cop"))
                {
                Debug.Log("~~ Cop Selected ~~");
                string name = hit.collider.gameObject.name;
                }
                
            }
        }
        */
        
        if (Input.GetButtonDown("Fire2") || Input.GetButton("Fire2"))
        {
            if (Physics.Raycast(ray, out hit) == true)
            {
                if(hit.transform.gameObject.CompareTag("Ground"))
                {
                    agentToMove = GameObject.Find("Cop");
                    agentToMove.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                }

            }
        }
        

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
