using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "NPCFaceProtagonist", menuName = "State Machines/Actions/NPC Face Protagonist")]
public class NPCFaceProtagonistSO : StateActionSO
{
    public TransformAnchor playerAnchor;

    protected override StateAction CreateAction()
    {
        return new NPCFaceProtagonist();
    }
}

public class NPCFaceProtagonist : StateAction
{
    private TransformAnchor _protagonist;
    private Transform       _actor;
    private Quaternion      rotationOnEnter;

    public override void Awake(StateMachine stateMachine)
    {
        _actor          = stateMachine.transform;
        _protagonist    = ((NPCFaceProtagonistSO)OriginSO).playerAnchor;
        rotationOnEnter = _actor.rotation;
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

    public override void OnStateExit()
    {
        _actor.rotation = rotationOnEnter;
    }
}