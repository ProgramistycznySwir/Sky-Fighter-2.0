
using UnityEngine;

public class FlakBullet_irl : MarchingBullet_irl
{
    public float proximitySensorRange = 10f;
    public float explosionRadius = 15f;
    public float distanceActivationRange = 30f;

    public GameObject particles;

    private int sphereCastLayer;
    private float distanceTraveled = 0;
    private bool armed = false;

    // Start is called before the first frame update
    void Start()
    {
        sphereCastLayer = LayerMask.NameToLayer("Player");
        lifespan = 5f;        
    }


    private RaycastHit[] hits;
    private System.Collections.Generic.List<string> hittedShips = new System.Collections.Generic.List<string>(); //for not inflicting damage several times to same ship (multicollider)
    // Update is called once per frame
    void FixedUpdate()
    {
        if (lifetime > lifespan) Die();

        hits = Physics.SphereCastAll(transform.position, proximitySensorRange, transform.forward, sphereCastLayer);

        if (hits.Length > 0 && armed)
        {
            float particleSysSize = explosionRadius * 0.01f;
            particles.transform.localScale = new Vector3(particleSysSize, particleSysSize, particleSysSize);
            particles.SetActive(true);
            
            foreach (RaycastHit hit in hits)
            {
                if (!hittedShips.Contains(hit.collider.name))
                {
                    hittedShips.Add(hit.collider.name);
                    hit.collider.transform.parent.GetComponentInParent<Stats>().ReceiveDamage(damage, hit.point);
                }                
            }
        }

        float marchDistance = velocity * Time.fixedDeltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out raycast, marchDistance) && matterialisationDelay <= 0)
        {
            transform.position = raycast.point;
            //if (raycast.collider.tag == "Player") raycast.collider.transform.parent.GetComponentInParent<Stats>().ReceiveDamage(damage, raycast.point);

            Die();
        }
        else
        {
            matterialisationDelay--;
            March();
        }


        lifetime += Time.fixedDeltaTime;
    }

    private void Explode()
    {

        Die();
    }

    public void SetStats(float damage, float velocity, Color trailColor, float proximitySensorRange)
    {
        this.damage = damage;
        this.velocity = velocity;
        trail.startColor = trailColor;
        trail.endColor = trailColor;

        this.proximitySensorRange = proximitySensorRange;
        explosionRadius = proximitySensorRange * 1.5f;

        distanceActivationRange = proximitySensorRange * 2f;
    }

    protected override void March()
    {
        transform.position += velocity * transform.forward * Time.fixedDeltaTime;
        distanceTraveled += velocity * Time.fixedDeltaTime;
        if (distanceTraveled > distanceActivationRange) armed = true;
    }
}
