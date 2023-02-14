using UnityEngine;
using UOP1.Factory;

[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Factory/SoundEmitter Factory")]
public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
{
    public SoundEmitter prefab;

    public override SoundEmitter Create()
    {
        return Instantiate(prefab);
    }
}