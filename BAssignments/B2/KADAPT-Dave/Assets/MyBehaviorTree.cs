using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree : MonoBehaviour
{
	//public Transform wander1;
	//public Transform wander2;
	//public Transform wander3;
    public GameObject CRIMINAL;
	public GameObject participant;
    public GameObject COP;
    private BehaviorAgent behaviorAgent;
    public GameObject button;
    public GameObject Door;

	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

    protected Node ApproachAndStare(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_OrientTowards(position), new LeafWait(1000));
    }

    protected Node ApproachAndStare2(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(CRIMINAL.GetComponent<BehaviorMecanim>().Node_OrientTowards(position), new LeafWait(1000));
    }

    protected Node HandsUp(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(CRIMINAL.GetComponent<BehaviorMecanim>().Node_BodyAnimation("DYING", true), new LeafWait(1000));
    }

    /*
    protected Node CriminalCrouchesNearCop(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(CRIMINAL.GetComponent<BehaviorMecanim>().Node_BodyAnimation("DYING", true), new LeafWait(1000));
    }
    */

    protected Node CopEngageCriminal (Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_HandAnimation("PISTOLAIM", true), new LeafWait(1000));
    }

    protected Node ApproachButton(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node PressButton(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_BodyAnimation("FIGHT", true), new LeafWait(1000));
    }
    
    protected Node BuildTreeRoot()
	{



        return new DecoratorLoop(

            new Sequence(
                //this.ST_ApproachAndWait(this.wander1),

                this.ApproachAndStare2(this.COP.transform)));
                    //this.ApproachAndStare2(this.COP.transform),
                   // this.CopEngageCriminal(this.CRIMINAL.transform),
                    //this.HandsUp(this.COP.transform)));
                    //this.ApproachButton(this.button.transform),
                    //this.ApproachAndStare(this.CRIMINAL.transform),
                   // this.PressButton(this.COP.transform)));
        //this.ST_ApproachAndWait(this.wander2),
        //this.ST_ApproachAndWait(this.wander3)));
        
    }
    
}
