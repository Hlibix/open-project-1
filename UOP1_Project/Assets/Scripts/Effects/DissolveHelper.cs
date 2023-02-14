using System.Collections;
using UnityEngine;

public class DissolveHelper : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _dissolveParticlesPrefab;

    [SerializeField]
    private float _dissolveDuration = 1f;

    private MeshRenderer   _renderer;
    private ParticleSystem _particules;

    private MaterialPropertyBlock _materialPropertyBlock;

    [ContextMenu("Trigger Dissolve")]
    public void TriggerDissolve()
    {
        if (_materialPropertyBlock == null)
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        InitParticleSystem();
        StartCoroutine(DissolveCoroutine());
    }

    [ContextMenu("Reset Dissolve")]
    private void ResetDissolve()
    {
        _materialPropertyBlock.SetFloat("_Dissolve", 0);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    private void InitParticleSystem()
    {
        _particules = Instantiate(_dissolveParticlesPrefab, transform);

        _renderer = GetComponent<MeshRenderer>();
        var shapeModule = _particules.shape;
        shapeModule.meshRenderer = _renderer;
        shapeModule.enabled      = true;

        var mainModule = _particules.main;
        mainModule.duration = _dissolveDuration;
    }

    public IEnumerator DissolveCoroutine()
    {
        float normalizedDeltaTime = 0;

        _particules.Play();

        while (normalizedDeltaTime < _dissolveDuration)
        {
            normalizedDeltaTime += Time.deltaTime;
            var remappedValue = VFXUtil.RemapValue(normalizedDeltaTime, 0, _dissolveDuration, 0, 1);
            _materialPropertyBlock.SetFloat("_Dissolve", remappedValue);
            _renderer.SetPropertyBlock(_materialPropertyBlock);

            yield return null;
        }

        Destroy(_particules.gameObject);
    }
}