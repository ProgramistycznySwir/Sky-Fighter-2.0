using UnityEngine;

public class FlakWeapon_irl : Weapon_irl
{
    // <FLA-00> - failed during SetTier()

    public GameObject projectile;



    public Transform barrelEnd;

    public float damage;

    public float firerate;

    public float projectileVelocity;

    public float explosionRadius;

    Color colorOfProjectiles;


    private float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetTier(tier);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown > 0f)
        {
            cooldown -= Time.fixedDeltaTime;
        }
    }

    public override void Fire()
    {
        if (cooldown <= 0f && weaponManager.cannonAmmo >= ammoConsumption && weaponManager.displayStats.stats.energy > energyConsumption)
        {
            GameObject newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
            newProjectile.GetComponent<FlakBullet_irl>().SetStats(damage, projectileVelocity, colorOfProjectiles, explosionRadius);

            weaponManager.displayStats.stats.energy -= energyConsumption;
            weaponManager.cannonAmmo -= ammoConsumption;
            cooldown = firerate;
        }
    }

    public override void SetTier(int tier)
    {
        this.tier = tier;
        SetColor(tier);
        colorOfProjectiles = TierColor(tier);
        energyConsumption = 0;

        switch (tier)
        {
            case 1:
                {
                    damage = 60f; //10
                    firerate = 0.5f;
                    projectileVelocity = 200f; //x0.5
                    ammoConsumption = 5;
                    explosionRadius = 7.5f;
                    break;
                }
            case 2:
                {
                    damage = 80f; //10
                    firerate = 0.45f;
                    projectileVelocity = 250f;
                    ammoConsumption = 7;
                    explosionRadius = 9f;
                    break;
                }
            case 3:
                {
                    damage = 100f; //10
                    firerate = 0.42f;
                    projectileVelocity = 275f;
                    ammoConsumption = 10;
                    explosionRadius = 10.5f;
                    break;
                }
            case 4:
                {
                    damage = 130f; //10
                    firerate = 0.4f;
                    projectileVelocity = 300f;
                    ammoConsumption = 13;
                    explosionRadius = 12f;
                    break;
                }
            case 5:
                {
                    damage = 160f; //10
                    firerate = 0.38f;
                    projectileVelocity = 325f;
                    ammoConsumption = 15;
                    explosionRadius = 13.5f;
                    break;
                }
            case 6:
                {
                    damage = 180f; //10
                    firerate = 0.36f;
                    projectileVelocity = 360f;
                    ammoConsumption = 17;
                    explosionRadius = 15.5f;
                    break;
                }
            case 7:
                {
                    damage = 200f; //10
                    firerate = 0.35f;
                    projectileVelocity = 400f;
                    ammoConsumption = 20;
                    explosionRadius = 17.5f;
                    break;
                }
            case 8:
                {
                    damage = 750f; //10
                    firerate = 1f;
                    projectileVelocity = 750f;
                    energyConsumption = 1000f;
                    ammoConsumption = 0;
                    explosionRadius = 30f;
                    break;
                }
            default:
                {
                    Debug.LogWarning(gameObject.name + ": <WPN:FLA-00> I have failed while trying to give a tier to this goddly weapon :<");
                    break;
                }
        }

        safeRange = explosionRadius * 2f;
        range = projectileVelocity * 5f; //5f - projectile lifespan; this is here for telemetry indicating out of range
    }
}
