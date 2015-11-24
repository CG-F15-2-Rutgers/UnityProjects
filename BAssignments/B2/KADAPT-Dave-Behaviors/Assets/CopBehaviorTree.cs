using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class CopBehaviorTree : MonoBehaviour
{
    public GameObject COP;
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

    protected Node PressButton()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_BodyAnimationButton("FIGHT", true), new LeafWait(1000));
    }

    protected Node GunIdle()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_HandAnimationIdle("PISTOLAIM", true), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
	{
        return new DecoratorLoop(
            new Sequence(
                PressButton(),
                GunIdle()));      
    }

   

}
