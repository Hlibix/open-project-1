using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannelSO _onSceneReady;

    [SerializeField]
    private AudioCueEventChannelSO _playMusicOn;

    [SerializeField]
    private GameSceneSO _thisSceneSO;

    [SerializeField]
    private AudioConfigurationSO _audioConfig;

    [Header("Pause menu music")]
    [SerializeField]
    private AudioCueSO _pauseMusic;

    [SerializeField]
    private BoolEventChannelSO _onPauseOpened;

    private void OnEnable()
    {
        _onPauseOpened.OnEventRaised += PlayPauseMusic;
        _onSceneReady.OnEventRaised  += PlayMusic;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised  -= PlayMusic;
        _onPauseOpened.OnEventRaised -= PlayPauseMusic;
    }

    private void PlayMusic()
    {
        _playMusicOn.RaisePlayEvent(_thisSceneSO.musicTrack, _audioConfig);
    }

    private void PlayPauseMusic(bool open)
    {
        if (open)
        {
            _playMusicOn.RaisePlayEvent(_pauseMusic, _audioConfig);
        }
        else
        {
            PlayMusic();
        }
    }
}