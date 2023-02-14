using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CritterFaceProtagonist", menuName = "State Machines/Actions/Critter Face Protagonist")]
public class CritterFaceProtagonistSO : StateActionSO
{
    public TransformAnchor playerAnchor;

    protected override StateAction CreateAction()
    {
        return new CritterFaceProtagonist();
    }
}

public class CritterFaceProtagonist : StateAction
{
    private TransformAnchor _protagonist;
    private Transform       _actor;

    public override void Awake(StateMachine stateMachine)
    {
        _actor       = stateMachine.transform;
        _protagonist = ((CritterFaceProtagonistSO)OriginSO).playerAnchor;
    }

    public override void OnUpdate()
    {
        if (_protagonist.isSet)
        {
            var relativePos = _protagonist.Value.position - _actor.position;
            relativePos.y = 0f; // Force rotation to be only on Y axis.

            var rotation = Quaternion.LookRotation(relativePos);
            _actor.rotation = rotation;
        }
    }

    public override void OnStateEnter()
    {
    }
}