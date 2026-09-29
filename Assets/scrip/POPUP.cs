using UnityEngine;

public class POPUP : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    void Update()
    {
        if(!ParticleSystem.isPlaying)
            ParticleSystem.Play();
        
    }

}
