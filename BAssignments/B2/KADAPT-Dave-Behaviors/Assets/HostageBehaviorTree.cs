using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class HostageBehaviorTree : MonoBehaviour
{
    public GameObject COP;
    public GameObject HOSTAGE;
    private BehaviorAgent behaviorAgent;

    // Use this for initialization
    void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();

    }

    protected Node DuckToCop()
    {
        return new Sequence(HOSTAGE.GetComponent<BehaviorMecanim>().Node_OrientTowardsHostage(),
            HOSTAGE.GetComponent<BehaviorMecanim>().Node_BodyAnimationHostage("DUCK", true), new LeafWait(1000));
    }

    protected Node RunAway()
    {
        return new Sequence(HOSTAGE.GetComponent<BehaviorMecanim>().Node_OrientTowardsHostage(),
            HOSTAGE.GetComponent<BehaviorMecanim>().Node_GoToHostage(), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
    {
        return new DecoratorLoop(
            new Sequence(
                RunAway()));
    }



}
