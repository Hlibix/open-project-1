using System.Collections;
using UnityEngine;

/// <summary>
/// Controls playback of particles connected to movement. Methods invoked by the StateMachine StateActions
/// </summary>
public class PlayerEffectController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _walkingParticles;

    [SerializeField]
    private ParticleSystem _landParticles;

    [SerializeField]
    private ParticleSystem _jumpParticles;

    [SerializeField]
    private ParticleSystem _slashEffect;

    [SerializeField]
    private ParticleSystem _reverseSlashEffect;

    private void Start()
    {
        _slashEffect.Stop();
        _reverseSlashEffect.Stop();
    }

    public void EnableWalkParticles()
    {
        _walkingParticles.Play();
    }

    public void DisableWalkParticles()
    {
        _walkingParticles.Stop();
    }

    public void PlayJumpParticles()
    {
        _jumpParticles.Play();
    }

    public void PlayLandParticles()
    {
        _landParticles.Play();
    }

    public void PlaySlashEffect()
    {
        _slashEffect.Play();
    }

    public void PlayReverseSlashEffect()
    {
        _reverseSlashEffect.Play();
    }

    public void PlayLandParticles(float intensity)
    {
        // make sure intensity is always between 0 and 1
        intensity = Mathf.Clamp01(intensity);

        var main      = _landParticles.main;
        var origCurve = main.startSize; //save original curve to be assigned back to particle system
        var newCurve  = main.startSize; //Make a new minMax curve and make our changes to the new copy

        var minSize = newCurve.constantMin;
        var maxSize = newCurve.constantMax;

        // use the intensity to change the maximum size of the particle curve
        newCurve.constantMax = Mathf.Lerp(minSize, maxSize, intensity);
        main.startSize       = newCurve;

        _landParticles.Play();

        // Put the original startSize back where you found it
        StartCoroutine(ResetMinMaxCurve(_landParticles, origCurve));

        // Note: We don't necessarily need to reset the curve, as it will be overridden
    }

    private IEnumerator ResetMinMaxCurve(ParticleSystem ps, ParticleSystem.MinMaxCurve curve)
    {
        while (ps.isEmitting)
        {
            yield return null;
        }

        var main = ps.main;
        main.startSize = curve;
    }
}