using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class CriminalBehaviorTree : MonoBehaviour
{
    public GameObject COP;
    public GameObject CRIMINAL;
    private BehaviorAgent behaviorAgent;

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

    protected Node DeathByCop()
    {
        return new Sequence(CRIMINAL.GetComponent<BehaviorMecanim>().Node_OrientTowardsCriminal(),
            CRIMINAL.GetComponent<BehaviorMecanim>().Node_BodyAnimationCriminal("DYING", true), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
	{
        return new DecoratorLoop(
            new Sequence(new LeafWait(1000),
                DeathByCop()));      
    }

   

}
