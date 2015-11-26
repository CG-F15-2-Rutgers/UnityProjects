using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class OutsideDudeBehaviorTree : MonoBehaviour
{
    public GameObject Dude;
    public GameObject Cop;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    protected Node PerformAction(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(Dude.GetComponent<BehaviorMecanim>().Node_HandAnimationHelpMe("CALLOVER", true), new LeafWait(1000));
    }

    protected Node PerformAction2(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(Dude.GetComponent<BehaviorMecanim>().Node_HandAnimationHelpMe("WAVE", true), new LeafWait(1000));
    }

    protected Node StareAtCop(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(Dude.GetComponent<BehaviorMecanim>().Node_OrientTowards(position), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
    {
        return
            new DecoratorLoop(
                new SequenceShuffle(
                    this.StareAtCop(this.Cop.transform),
                    this.PerformAction(this.Dude.transform),
                    this.PerformAction2(this.Dude.transform),
                    this.StareAtCop(this.Cop.transform)));
    }
}