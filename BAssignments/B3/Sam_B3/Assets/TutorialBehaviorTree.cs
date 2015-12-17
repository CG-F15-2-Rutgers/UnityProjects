using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class TutorialBehaviorTree : MonoBehaviour
{
	public GameObject participant;

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

    protected Node ST_Sigh()
    {
        return new Sequence(new LeafWait(6000),
            participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("YAWN",6000));
    }

    protected Node BuildTreeRoot()
	{
        return new DecoratorLoop(
            new Sequence(this.ST_Sigh()));
    }
}
