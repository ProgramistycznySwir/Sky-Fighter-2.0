using UnityEngine;

public class MarchingBullet_irl : MonoBehaviour
{
    public TrailRenderer trail;

    public int matterialisationDelay = 0;

    public float damage = 10f;

    public float velocity = 1000f;
    [Tooltip("Time in seconds that projectile lasts")]
    public float lifespan = 2;
    [Tooltip("If true projectile will abbandon it's actions and proceed to decay")]
    public float decayTime = 0.5f;
    protected float lifetime = 0f;


    protected RaycastHit raycast;

    void FixedUpdate()
    {
        if (lifetime > lifespan) Die();
        
        float marchDistance = velocity * Time.fixedDeltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out raycast, marchDistance) && matterialisationDelay <= 0)
        {
            transform.position = raycast.point;
            if(raycast.collider.tag == "Player") raycast.collider.transform.parent.GetComponentInParent<Stats>().ReceiveDamage(damage, raycast.point);

            Die();                
        }
        else
        {
            matterialisationDelay--;
            March();
        }


        lifetime += Time.fixedDeltaTime;
    }

    protected virtual void March()
    {
        transform.position += velocity * transform.forward * Time.fixedDeltaTime;
    }
    protected void Die()
    {
        Destroy(gameObject, decayTime);
        Destroy(this);
    }
    public void SetStats(float damage, float velocity, Color trailColor)
    {
        this.damage = damage;
        this.velocity = velocity;
        trail.startColor = trailColor;
        trail.endColor = trailColor;
    }
}
