using UnityEngine;

public class FireObject : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    
    public float fireIntensity = 100f; 

    public void ReduceFire(float amount)
    {
        fireIntensity -= amount;
        if (fireIntensity <= 0f)
        {
            ExtinguishFire();
        }
        else
        {
            UpdateFireEffect();
        }
    }

    private void ExtinguishFire()
    {
        fireParticleSystem.Stop();
    }

    private void UpdateFireEffect()
    {
        var emission = fireParticleSystem.emission;
        emission.rateOverTime = fireIntensity * 0.5f;

    }
}