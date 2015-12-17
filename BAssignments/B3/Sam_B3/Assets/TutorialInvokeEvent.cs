using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using System;

public class TutorialInvokeEvent : MonoBehaviour
{

    public GameObject Friend;
    public GameObject Wanderer;
    public RButtonPressed rButton;

    private BehaviorAgent behaviorAgent;

    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.ConversationTree());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node Converse()
    {
        Debug.Log("Converse Active!");
        return new Sequence(Wanderer.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 2000), 
            Friend.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("WAVE", (long)2000),
            Wanderer.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("BEINGCOCKY", 2000), 
            Friend.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HEADNOD", 2000));
    }

    protected Node ApproachAndOrient()
    {
        Debug.Log("Approach and Orient Active!");
        Val<Vector3> FriendPos = Val.V(() => Friend.transform.position);
        Val<Vector3> WandererPos = Val.V(() => Wanderer.transform.position);
        return new Sequence(new SequenceParallel(             
            // Approach at distance 1.0f             
            Wanderer.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(FriendPos, 1.0f)),
            new SequenceParallel( 
                Friend.GetComponent<BehaviorMecanim>().Node_OrientTowards(WandererPos), 
                Wanderer.GetComponent<BehaviorMecanim>().Node_OrientTowards(FriendPos)));
    }

    protected Node waitForRButtonPressed()
    {
        return new DecoratorInvert(
            new DecoratorLoop((new DecoratorInvert(
            new Sequence(new LeafAssert(isRPressed))))));
    }

    bool isRPressed()
    {
        Val<bool> r = Val.V(() => this.rButton.rPressed);
        return r.Value;
    }

    protected RunStatus resetRPressed()
    {
        rButton.rPressed = false;
        return RunStatus.Success;
    }

    public Node ConversationTree()
    {
        return new DecoratorLoop(
            new Sequence(this.waitForRButtonPressed(),
                new LeafWait(1000), 
                this.ApproachAndOrient(),
                this.Converse(),
                new LeafInvoke((Func<RunStatus>)resetRPressed)));
    }


}
