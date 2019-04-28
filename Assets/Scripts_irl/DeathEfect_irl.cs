using UnityEngine;

public class DeathEfect_irl : MonoBehaviour
{
    public float stayForSeconds = 15f;
    public float lightDecayRate = 0.1f;
    public float particlesDecayRate = 0.1f;
    public Light light;
    public ParticleSystem particles;

    void Start()
    {
        Destroy(gameObject, stayForSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity *= 1 - (lightDecayRate * Time.deltaTime);
        particles.emissionRate *= 1 - (lightDecayRate * Time.deltaTime);
    }
}
