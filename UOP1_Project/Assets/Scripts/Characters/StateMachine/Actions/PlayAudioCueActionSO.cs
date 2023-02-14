using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Actions/Play AudioCue")]
public class PlayAudioCueActionSO : StateActionSO<PlayAudioCueAction>
{
    public AudioCueSO             audioCue;
    public AudioCueEventChannelSO audioCueEventChannel;
    public AudioConfigurationSO   audioConfiguration;
}

public class PlayAudioCueAction : StateAction
{
    private Transform _stateMachineTransform;

    private PlayAudioCueActionSO _originSO => (PlayAudioCueActionSO)OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _stateMachineTransform = stateMachine.transform;
    }

    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        _originSO.audioCueEventChannel.RaisePlayEvent(_originSO.audioCue, _originSO.audioConfiguration, _stateMachineTransform.position);
    }
}