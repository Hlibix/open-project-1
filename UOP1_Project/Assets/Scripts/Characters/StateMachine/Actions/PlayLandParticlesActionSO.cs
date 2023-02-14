using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Actions/Play Land Particles")]
public class PlayLandParticlesActionSO : StateActionSO<PlayLandParticlesAction>
{
}

public class PlayLandParticlesAction : StateAction
{
    //Component references
    private PlayerEffectController _dustController;
    private Transform              _transform;
    private CharacterController    _characterController;

    private float _coolDown = 0.3f;
    private float t;

    private float _fallStartY;
    private float _fallEndY;
    private float _maxFallDistance = 4f; //Used to adjust particle emission intensity

    public override void Awake(StateMachine stateMachine)
    {
        _dustController      = stateMachine.GetComponent<PlayerEffectController>();
        _transform           = stateMachine.transform;
        _characterController = stateMachine.GetComponent<CharacterController>();
    }

    public override void OnStateEnter()
    {
        _fallStartY = _transform.position.y;
    }

    public override void OnStateExit()
    {
        _fallEndY = _transform.position.y;
        var dY            = Mathf.Abs(_fallStartY - _fallEndY);
        var fallIntensity = Mathf.InverseLerp(0, _maxFallDistance, dY);

        if (Time.time >= t + _coolDown && _characterController.isGrounded)
        {
            _dustController.PlayLandParticles(fallIntensity);
            t = Time.time;
        }
    }

    public override void OnUpdate()
    {
    }
}