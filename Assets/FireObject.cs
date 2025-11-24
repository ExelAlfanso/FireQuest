using UnityEngine;

public class FireObject : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    public float fireIntensity = 100f; // Represents how intense the fire is

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
        // Additional logic for extinguishing the fire can be added here
    }

    private void UpdateFireEffect()
    {
        var emission = fireParticleSystem.emission;
        emission.rateOverTime = fireIntensity;
    }
}