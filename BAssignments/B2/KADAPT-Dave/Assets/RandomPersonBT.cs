using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class RandomPersonBT : MonoBehaviour
{
    public GameObject participant;
    public GameObject Cop;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node PerformAction(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_HandAnimation("CALLOVER", true), new LeafWait(1000));
    }

    protected Node PerformAction2(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true), new LeafWait(1000));
    }

    protected Node StareAtCop(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_OrientTowards(position), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
    {
        return
            new DecoratorLoop(
                new SequenceShuffle(
                    this.StareAtCop(this.Cop.transform),
                    this.PerformAction(this.participant.transform),
                    this.PerformAction2(this.participant.transform),
                    this.StareAtCop(this.Cop.transform)));
    }
}