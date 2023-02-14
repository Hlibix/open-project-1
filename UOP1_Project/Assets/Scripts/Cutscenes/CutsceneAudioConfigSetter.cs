using UnityEngine;

public class CutsceneAudioConfigSetter : MonoBehaviour
{
    [SerializeField]
    private AudioConfigurationSO _audioConfig;

    [SerializeField]
    private VoidEventChannelSO onCutsceneStart;

    private void OnEnable()
    {
        onCutsceneStart.OnEventRaised += SetVolume;
    }

    private void OnDestroy()
    {
        onCutsceneStart.OnEventRaised -= SetVolume;
    }

    private void SetVolume()
    {
        GetComponent<AudioSource>().volume = _audioConfig.Volume;
    }
}