using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class TutorialInvokeEvent : MonoBehaviour
{

    public GameObject Friend;
    public GameObject Wanderer;

    private BehaviorAgent behaviorAgent;

    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.ConversationTree());
        BehaviorManager.Instance.Register(behaviorAgent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            behaviorAgent.StartBehavior();
        }
    }
    protected Node Converse()
    {
        return new Sequence(Wanderer.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE",2000), 
            Friend.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("WAVE", 2000),
            Wanderer.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("BEINGCOCKY", 2000), 
            Friend.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HEADNOD", 2000));
    }

    protected Node EyeContact(Val<Vector3> WandererPos, Val<Vector3> FriendPos)
    {         
        // Estimate the head position based on height         
        Vector3 height = new Vector3(0.0f, 1.85f, 0.0f);  

        Val<Vector3> WandererHead = Val.V(() => WandererPos.Value + height);
        Val<Vector3> FriendHead = Val.V(() => FriendPos.Value + height);
        return new SequenceParallel(Friend.GetComponent<BehaviorMecanim>().Node_HeadLook(WandererHead), 
            Wanderer.GetComponent<BehaviorMecanim>().Node_HeadLook(FriendHead));
    }

    protected Node EyeContactAndConverse(Val<Vector3> WandererPos, Val<Vector3> FriendPos)
    {
        return new Race(this.EyeContact(WandererPos, FriendPos), 
            this.Converse());
    }

    protected Node ApproachAndOrient(Val<Vector3> WandererPos, Val<Vector3> FriendPos)
    {
        return new Sequence(             
            // Approach at distance 1.0f             
            Friend.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(WandererPos, 1.0f),             
            new SequenceParallel( 
                Friend.GetComponent<BehaviorMecanim>().Node_OrientTowards(WandererPos), 
                Wanderer.GetComponent<BehaviorMecanim>().Node_OrientTowards(FriendPos)));
    }

    public Node ConversationTree()
    {
        Val<Vector3> WandererPos = Val.V(() => Wanderer.transform.position);
        Val<Vector3> FriendPos = Val.V(() => Friend.transform.position);
        return new Sequence(this.ApproachAndOrient(WandererPos, FriendPos), 
            this.EyeContactAndConverse(WandererPos, FriendPos));
    }


}
