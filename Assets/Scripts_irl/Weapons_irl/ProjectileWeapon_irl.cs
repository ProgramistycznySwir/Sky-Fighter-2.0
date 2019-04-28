using UnityEngine;

public class ProjectileWeapon_irl : Weapon_irl
{
    // <PRO-00> - failed during SetTier()

    public GameObject projectile;

    

    public Transform barrelEnd;

    public float damage;

    public float firerate;

    public float projectileVelocity;

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
        if(cooldown > 0f)
        {
            cooldown -= Time.fixedDeltaTime;
        }
    }

    public override void Fire()
    {
        if(cooldown <= 0f && weaponManager.cannonAmmo > 0 && weaponManager.displayStats.stats.energy > energyConsumption)
        {
            GameObject newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
            newProjectile.GetComponent<MarchingBullet_irl>().SetStats(damage, projectileVelocity, colorOfProjectiles);

            weaponManager.displayStats.stats.energy -= energyConsumption;
            weaponManager.cannonAmmo--;
            cooldown = firerate;
        }
    }

    public override void SetTier(int tier)
    {
        this.tier = tier;
        SetColor(tier);
        colorOfProjectiles = TierColor(tier);
        energyConsumption = 0;
        ammoConsumption = 1;

        switch (tier)
        {
            case 1:
                {
                    damage = 50f; //10
                    firerate = 0.2f;
                    projectileVelocity = 600f; //x0.5
                    break;
                }
            case 2:
                {
                    damage = 75f; //10
                    firerate = 0.175f;
                    projectileVelocity = 700f;
                    break;
                }
            case 3:
                {
                    damage = 90f; //10
                    firerate = 0.16f;
                    projectileVelocity = 800f;
                    break;
                }
            case 4:
                {
                    damage = 105f; //10
                    firerate = 0.15f;
                    projectileVelocity = 900f;
                    break;
                }
            case 5:
                {
                    damage = 120f; //10
                    firerate = 0.14f;
                    projectileVelocity = 1100f;
                    break;
                }
            case 6:
                {
                    damage = 140f; //10
                    firerate = 0.13f;
                    projectileVelocity = 1300f;
                    break;
                }
            case 7:
                {
                    damage = 2000f; //10
                    firerate = 1f;
                    projectileVelocity = 4000f;
                    energyConsumption = 750f;
                    break;
                }
            case 8:
                {
                    damage = 30f; //10
                    firerate = 0f;
                    projectileVelocity = 8000f;
                    energyConsumption = 5f;
                    break;
                }
            default:
                {
                    Debug.LogWarning(gameObject.name + ": <WPN:PRO-00> I have failed while trying to give a tier to this goddly weapon :<");
                    break;
                }
        }

        range = projectileVelocity * 2f; //2f - projectile lifespan; this is here for telemetry indicating out of range
    }
}
