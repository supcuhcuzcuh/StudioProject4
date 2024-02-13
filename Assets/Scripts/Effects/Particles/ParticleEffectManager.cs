using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    [SerializeField]
    private List<SpawnParticleEffect> spawnParticleEffectList = new List<SpawnParticleEffect>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool PlayParticleEffect(string _particleEffectName, Vector3 _position)
    {
        foreach (SpawnParticleEffect particleEffect in spawnParticleEffectList)
        {
            if (particleEffect.particleEffectName == _particleEffectName)
            {
                Instantiate(particleEffect.particleEffect, _position, Quaternion.identity);
            
                return true;
            }
        }
        Debug.Log("Particle effect not found");
        return false;
    }

    public bool PlayParticleEffect(string _particleEffectName, Vector3 _position, Transform _objectToFollow)
    {
        foreach (SpawnParticleEffect particleEffect in spawnParticleEffectList)
        {
            if (particleEffect.particleEffectName == _particleEffectName)
            {
                ParticleSystem newparticleEffect = Instantiate(particleEffect.particleEffect, _position, Quaternion.identity);
                newparticleEffect.transform.SetParent(_objectToFollow);
                newparticleEffect.transform.localRotation = Quaternion.Euler(Vector3.zero);
                return true;
            }
        }
        Debug.Log("Particle effect not found");
        return false;
    }

}
