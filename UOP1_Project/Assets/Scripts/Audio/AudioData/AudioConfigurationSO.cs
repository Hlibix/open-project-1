using UnityEngine;
using UnityEngine.Audio;

//TODO: Check which settings we really need at this level
[CreateAssetMenu(menuName = "Audio/Audio Configuration")]
public class AudioConfigurationSO : ScriptableObject
{
    public AudioMixerGroup OutputAudioMixerGroup;

    // Simplified management of priority levels (values are counterintuitive, see enum below)
    [SerializeField]
    private PriorityLevel _priorityLevel = PriorityLevel.Standard;

    [HideInInspector]
    public int Priority
    {
        get => (int)_priorityLevel;
        set => _priorityLevel = (PriorityLevel)value;
    }

    [Header("Sound properties")]
    public bool Mute;

    [Range(0f, 1f)]
    public float Volume = 1f;

    [Range(-3f, 3f)]
    public float Pitch = 1f;

    [Range(-1f, 1f)]
    public float PanStereo;

    [Range(0f, 1.1f)]
    public float ReverbZoneMix = 1f;

    [Header("Spatialisation")]
    [Range(0f, 1f)]
    public float SpatialBlend = 1f;

    public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;

    [Range(0.01f, 5f)]
    public float MinDistance = 0.1f;

    [Range(5f, 100f)]
    public float MaxDistance = 50f;

    [Range(0, 360)]
    public int Spread;

    [Range(0f, 5f)]
    public float DopplerLevel = 1f;

    [Header("Ignores")]
    public bool BypassEffects;

    public bool BypassListenerEffects;
    public bool BypassReverbZones;
    public bool IgnoreListenerVolume;
    public bool IgnoreListenerPause;

    private enum PriorityLevel
    {
        Highest  = 0,
        High     = 64,
        Standard = 128,
        Low      = 194,
        VeryLow  = 256
    }

    public void ApplyTo(AudioSource audioSource)
    {
        audioSource.outputAudioMixerGroup = OutputAudioMixerGroup;
        audioSource.mute                  = Mute;
        audioSource.bypassEffects         = BypassEffects;
        audioSource.bypassListenerEffects = BypassListenerEffects;
        audioSource.bypassReverbZones     = BypassReverbZones;
        audioSource.priority              = Priority;
        audioSource.volume                = Volume;
        audioSource.pitch                 = Pitch;
        audioSource.panStereo             = PanStereo;
        audioSource.spatialBlend          = SpatialBlend;
        audioSource.reverbZoneMix         = ReverbZoneMix;
        audioSource.dopplerLevel          = DopplerLevel;
        audioSource.spread                = Spread;
        audioSource.rolloffMode           = RolloffMode;
        audioSource.minDistance           = MinDistance;
        audioSource.maxDistance           = MaxDistance;
        audioSource.ignoreListenerVolume  = IgnoreListenerVolume;
        audioSource.ignoreListenerPause   = IgnoreListenerPause;
    }
}