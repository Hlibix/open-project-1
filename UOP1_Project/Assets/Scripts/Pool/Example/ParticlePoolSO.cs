using UnityEngine;
using UOP1.Factory;
using UOP1.Pool;

[CreateAssetMenu(fileName = "NewParticlePool", menuName = "Pool/Particle Pool")]
public class ParticlePoolSO : ComponentPoolSO<ParticleSystem>
{
    [SerializeField]
    private ParticleFactorySO _factory;

    public override IFactory<ParticleSystem> Factory
    {
        get => _factory;
        set => _factory = value as ParticleFactorySO;
    }
}