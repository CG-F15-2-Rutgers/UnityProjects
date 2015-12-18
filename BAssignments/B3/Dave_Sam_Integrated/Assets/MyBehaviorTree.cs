using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree : MonoBehaviour
{
	public GameObject Player;
	public GameObject Prisoner;
    public GameObject Cop1;
    public GameObject Cop2;
    public GameObject Cop3;
    public GameObject Cop4;
    public GameObject Cop5;
    public GameObject Cop6;

    public Transform Wander1Start;
    public Transform Wander1End;
    public Transform Wander2Start;
    public Transform Wander2End;
    public Transform Wander3Start;
    public Transform Wander3End;
    public Transform Wander4Start;
    public Transform Wander4End;
    public Transform Wander5Start;
    public Transform Wander5End;
    public Transform Wander6Start;
    public Transform Wander6End;
    public Transform PlayerStartPoint;
    public Transform PlayerStartOrientation;

    public PlayerControls Controls;
    public CameraController Camera;
    public TextBoxController TextBox;

    private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BehaviorTree());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    bool returnTrue()
    {
        return true;
    }

    protected Node PrisonerFollow(GameObject Player, GameObject Prisoner)
    {
        Val<Vector3> PlayerPos = Val.V(() => Player.transform.position);
        return new Sequence(
            Prisoner.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(PlayerPos, 2.0f));
    }

    protected Node InitialConversation(GameObject Player, GameObject Prisoner)
    {
        Val<Vector3> PlayerPos = Val.V(() => Player.transform.position);
        Val<Vector3> PrisonerPos = Val.V(() => Prisoner.transform.position);
        Val<Vector3> StartPos = Val.V(() => PlayerStartPoint.position);
        Val<Vector3> StartOrient = Val.V(() => PlayerStartOrientation.position);
        return new Sequence(
            new LeafWait(4000),
            new SequenceParallel(Player.GetComponent<BehaviorMecanim>().Node_OrientTowards(PrisonerPos),
                                 Prisoner.GetComponent<BehaviorMecanim>().Node_OrientTowards(PlayerPos)),
            new LeafInvoke((Func<RunStatus>)TextBox.startCounter),
            new SequenceParallel(Player.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 2000),
                                 new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished))))))),
            new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
            new SequenceParallel(Prisoner.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("WAVE", (long)2000),
                                 new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished))))))),
            new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
            new SequenceParallel(Player.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("BEINGCOCKY", 2000),
                                 new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished))))))),
            new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
            new SequenceParallel(Prisoner.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HEADNOD", 2000),
                                 new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished))))))),
            new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
            Player.GetComponent<BehaviorMecanim>().Node_GoTo(StartPos),
            Player.GetComponent<BehaviorMecanim>().Node_OrientTowards(StartOrient),
            new LeafInvoke((Func<RunStatus>)Controls.EnableControls),
            new LeafInvoke((Func<RunStatus>)Camera.EnableCameraFollow),
            new DecoratorLoop(PrisonerFollow(Player,Prisoner)));
    }

    protected Node CopWander(GameObject Cop, Transform WanderBegin, Transform WanderEnd)
    {
        Val<Vector3> begin = Val.V(() => WanderBegin.position);
        Val<Vector3> end = Val.V(() => WanderEnd.position);
        return new DecoratorLoop(
            new Sequence(Cop.GetComponent<BehaviorMecanim>().Node_GoTo(begin), new LeafWait(1000),
                         Cop.GetComponent<BehaviorMecanim>().Node_GoTo(end), new LeafWait(1000)));
    }

	/*protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

	protected Node BuildTreeRoot()
	{
		Val<float> pp = Val.V (() => police.transform.position.z);
		Func<bool> act = () => (police.transform.position.z > 10);
		Node roaming = new DecoratorLoop (
						new Sequence(
						this.ST_ApproachAndWait(this.wander1),
						this.ST_ApproachAndWait(this.wander2),
						this.ST_ApproachAndWait(this.wander3)));
		Node trigger = new DecoratorLoop (new LeafAssert (act));
		Node root = new DecoratorLoop (new DecoratorForceStatus (RunStatus.Success, new SequenceParallel(trigger, roaming)));
		return root;
	}*/

    protected Node InitialConversationTree()
    {
        return new DecoratorLoop(
            new DecoratorForceStatus(RunStatus.Success, InitialConversation(Player,Prisoner)));
    }

    protected Node BehaviorTree()
    {
        return new DecoratorLoop(
            new SequenceParallel(
                InitialConversationTree(),
                CopWander(Cop1, Wander1Start, Wander1End),
                CopWander(Cop2, Wander2Start, Wander2End),
                CopWander(Cop3, Wander3Start, Wander3End),
                CopWander(Cop4, Wander4Start, Wander4End),
                CopWander(Cop5, Wander5Start, Wander5End),
                CopWander(Cop6, Wander6Start, Wander6End)));
    }

}
