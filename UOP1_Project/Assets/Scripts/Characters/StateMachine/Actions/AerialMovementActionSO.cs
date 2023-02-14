using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

/// <summary>
/// This Action handles horizontal movement while in the air, keeping momentum, simulating air resistance, and accelerating towards the desired speed.
/// </summary>
[CreateAssetMenu(fileName = "AerialMovement", menuName = "State Machines/Actions/Aerial Movement")]
public class AerialMovementActionSO : StateActionSO
{
    public float Speed        => _speed;
    public float Acceleration => _acceleration;

    [Tooltip("Desired horizontal movement speed while in the air")]
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _speed = 10f;

    [Tooltip("The acceleration applied to reach the desired speed")]
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _acceleration = 20f;

    protected override StateAction CreateAction()
    {
        return new AerialMovementAction();
    }
}

public class AerialMovementAction : StateAction
{
    private new AerialMovementActionSO OriginSO => (AerialMovementActionSO)base.OriginSO;

    private Protagonist _protagonist;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override void OnUpdate()
    {
        var velocity     = _protagonist.movementVector;
        var input        = _protagonist.movementInput;
        var speed        = OriginSO.Speed;
        var acceleration = OriginSO.Acceleration;

        SetVelocityPerAxis(ref velocity.x, input.x, acceleration, speed);
        SetVelocityPerAxis(ref velocity.z, input.z, acceleration, speed);

        _protagonist.movementVector = velocity;
    }

    private void SetVelocityPerAxis(ref float currentAxisSpeed, float axisInput, float acceleration, float targetSpeed)
    {
        if (axisInput == 0f)
        {
            if (currentAxisSpeed != 0f)
            {
                ApplyAirResistance(ref currentAxisSpeed);
            }
        }
        else
        {
            var (absVel, absInput)   =  (Mathf.Abs(currentAxisSpeed), Mathf.Abs(axisInput));
            var (signVel, signInput) =  (Mathf.Sign(currentAxisSpeed), Mathf.Sign(axisInput));
            targetSpeed              *= absInput;

            if (signVel != signInput || absVel < targetSpeed)
            {
                currentAxisSpeed += axisInput * acceleration;
                currentAxisSpeed =  Mathf.Clamp(currentAxisSpeed, -targetSpeed, targetSpeed);
            }
            else
            {
                ApplyAirResistance(ref currentAxisSpeed);
            }
        }
    }

    private void ApplyAirResistance(ref float value)
    {
        var sign = Mathf.Sign(value);

        value -= sign * Protagonist.AIR_RESISTANCE * Time.deltaTime;
        if (Mathf.Sign(value) != sign)
        {
            value = 0;
        }
    }
}