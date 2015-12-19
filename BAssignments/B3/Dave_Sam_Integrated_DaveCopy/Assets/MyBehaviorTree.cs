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
    public GameObject Scanner;

    private Val<GameObject> CopClicked;

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
    public TreasureBoxMovement Treasure;
    public GameObject Poster;
    public PoliceCollisionAggregator PCA;
    public ScannerCollision ScannerCol;

    private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
        CopClicked = Val.V(() => new GameObject());
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
        Val<Vector3> ChestPos = Val.V(() => Treasure.TreasureBox.transform.position);
        return new Sequence(
            new LeafWait(1000),
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
            new SequenceParallel(new DecoratorLoop(PrisonerFollow(Player,Prisoner)),
                                 new Sequence(new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(Controls.isTreasureClicked)))))),
                                              Player.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(Treasure.TreasureBox.transform.position, 2f),
                                              Player.GetComponent<BehaviorMecanim>().Node_OrientTowards(Treasure.TreasureBox.transform.position),
                                              new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                                              new LeafInvoke((Func<RunStatus>)TextBox.TreasureDialog),
                                              new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished)))))),
                                              new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                                              Player.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("REACHRIGHT", 1000),
                                              new LeafWait(500),
                                              new LeafInvoke((Func<RunStatus>)Treasure.makeTreasureMove),
                                              new LeafInvoke((Func<RunStatus>)TextBox.TreasureDialogComplete),
                                              new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished)))))),
                                              new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                                              new LeafWait(500)),
                                 new Sequence(new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(Controls.isPosterClicked)))))),
                                              Player.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(Poster.transform.position, 2f),
                                              Player.GetComponent<BehaviorMecanim>().Node_OrientTowards(Poster.transform.position),
                                              new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                                              new LeafInvoke((Func<RunStatus>)TextBox.PosterDialog),
                                              new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished)))))),
                                              new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                                              new LeafWait(500),
                                              Player.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("REACHRIGHT", 1000),
                                              new LeafInvoke((Func<RunStatus>)TextBox.PosterDialogComplete),
                                              new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished)))))),
                                              new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                                              new LeafWait(500))));
    }

    protected Node CopOrient(GameObject Cop, GameObject Player)
    {
        Val<Vector3> player = Val.V(() => Player.transform.position);
        return new SequenceParallel(Cop.GetComponent<BehaviorMecanim>().Node_OrientTowards(player),
                                 Cop.GetComponent<BehaviorMecanim>().Node_HandAnimation("PISTOLAIM", true));
    }

    protected RunStatus LostGame()
    {
        Time.timeScale = 0.0f;
        behaviorAgent.StopBehavior();
        return RunStatus.Success;
    }

    protected Node CopWander(GameObject Cop, Transform WanderBegin, Transform WanderEnd)
    {
        Val<Vector3> begin = Val.V(() => WanderBegin.position);
        Val<Vector3> end = Val.V(() => WanderEnd.position);
        return new Race(
                new Sequence(
                    new Race(
                        new DecoratorLoop(
                            new Sequence(Cop.GetComponent<BehaviorMecanim>().Node_GoTo(begin), new LeafWait(1000),
                                 Cop.GetComponent<BehaviorMecanim>().Node_GoTo(end), new LeafWait(1000))),
                        new Sequence(new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(PCA.hasPrisonerCollided)))))),
                            new LeafWait(500))),
                    new LeafInvoke((Func<RunStatus>)Controls.DisableControls),
                    new SequenceParallel(
                        new Sequence(new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                            new LeafInvoke((Func<RunStatus>)TextBox.CaughtDialog),
                            new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(TextBox.isDialogFinished)))))),
                            new LeafInvoke((Func<RunStatus>)TextBox.resetDialogFinished),
                            new LeafInvoke((Func<RunStatus>)LostGame)),
                        Player.GetComponent<BehaviorMecanim>().Node_HandAnimation("HANDSUP", true),
                        Prisoner.GetComponent<BehaviorMecanim>().Node_HandAnimation("HANDSUP", true),
                        CopOrient(Cop1, Player),
                        CopOrient(Cop2, Player),
                        CopOrient(Cop3, Player),
                        CopOrient(Cop4, Player),
                        CopOrient(Cop5, Player),
                        CopOrient(Cop6, Player))));
    }

    protected Node InitialConversationTree()
    {
        return new DecoratorLoop(
            new DecoratorForceStatus(RunStatus.Success, InitialConversation(Player,Prisoner)));
    }

    protected Node UnlockExit(GameObject Player, GameObject Scanner)
    {
        Val<Vector3> scannerPos = Val.V(() => Scanner.transform.position);
        return new Sequence(
            new DecoratorInvert(new DecoratorLoop((new DecoratorInvert(new Sequence(new LeafAssert(Controls.isScannerClicked)))))),
            Player.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(scannerPos, 2f),
            Player.GetComponent<BehaviorMecanim>().Node_OrientTowards(scannerPos),
            new LeafWait(500),
            Player.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("REACHRIGHT", 1000),
            new LeafWait(500));
    }

    protected Node BehaviorTree()
    {
        return new DecoratorLoop(
            new SequenceParallel(
                InitialConversationTree(),
                UnlockExit(Player, Scanner),
                CopWander(Cop1, Wander1Start, Wander1End),
                CopWander(Cop2, Wander2Start, Wander2End),
                CopWander(Cop3, Wander3Start, Wander3End),
                CopWander(Cop4, Wander4Start, Wander4End),
                CopWander(Cop5, Wander5Start, Wander5End),
                CopWander(Cop6, Wander6Start, Wander6End)));
    }

}
