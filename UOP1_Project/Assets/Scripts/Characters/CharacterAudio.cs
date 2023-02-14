using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    protected AudioCueEventChannelSO _sfxEventChannel;

    [SerializeField]
    protected AudioConfigurationSO _audioConfig;

    [SerializeField]
    protected GameStateSO _gameState;

    protected void PlayAudio(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace = default)
    {
        if (_gameState.CurrentGameState != GameState.Cutscene)
        {
            _sfxEventChannel.RaisePlayEvent(audioCue, audioConfiguration, positionInSpace);
        }
    }
}