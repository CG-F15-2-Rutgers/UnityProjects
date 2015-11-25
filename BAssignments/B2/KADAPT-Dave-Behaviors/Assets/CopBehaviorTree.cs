using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class CopBehaviorTree : MonoBehaviour
{
    public GameObject COP;
    private BehaviorAgent behaviorAgent;
    private Transform target;

    // Use this for initialization
    void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();

    }

    protected Node PressButton()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_HandAnimationButton("REACHRIGHT", true));
    }

    protected Node WaveToHelpMeDude()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_HandAnimationHelpMe2("WAVE", true), new LeafWait(1000));
    }

    protected Node WaveToCriminal()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_OrientTowardsCriminal(),
            COP.GetComponent<BehaviorMecanim>().Node_HandAnimationCriminal("WAVE", true), new LeafWait(1000));
    }

    protected Node WaveToHostage()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_OrientTowardsHostage(),
            COP.GetComponent<BehaviorMecanim>().Node_HandAnimationHostage("WAVE", true), new LeafWait(1000));
    }

    protected Node FightCriminal()
    {
        return new Sequence(COP.GetComponent<BehaviorMecanim>().Node_OrientTowardsCriminal(),
            COP.GetComponent<BehaviorMecanim>().Node_HandAnimationCriminal("PISTOLAIM", true), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
	{
        return new DecoratorLoop(
            new Sequence(
                WaveToHelpMeDude(),
                FightCriminal(),
                WaveToHostage(),
                PressButton()));      
    }

   

}
